using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SlimeBehaviour : StateMachineBase
{
    [SerializeField] SlimeData _data;
    [SerializeField] float _grabbableDuration;
    [SerializeField] FlipObjectToPoint _flip;

    float _currentHp;
    SlimeMovement _movement;
    KnockbackController _knockback;
    public bool IsGrabbable { get { return CurrentState is SlimeStates.GrabbableState; } }
    public bool IsAlive { get { return !(CurrentState is SlimeStates.DeadState || CurrentState is SlimeStates.GrabbableState ||
                                        CurrentState is SlimeStates.GrabbedState); } }
    public float GrabbableDuration { get { return _grabbableDuration; } }
    public Sprite SlotIcon { get { return _data.SlotIcon; } }

    public SlimeBulletEffect BulletEffect { get; private set; }
    public FlipObjectToPoint Flipper { get { return _flip; } }


    public float MoveSpeed { get { return _data.MoveSpeed; } }
    public float DamageAsBullet { get { return _data.DamageAsBullet; } }
    public float AttackRange { get { return _data.AttackRange; } }
    public float AttackPower { get { return _data.AttackPower; } }
    public float AttackCoolTime { get { return 1f / _data.AttackSpeed; } }
    public float SightRange { get { return _data.SightRange; } }

    private void Awake()
    {
        BulletEffect = GetComponent<SlimeBulletEffect>();
        _movement = GetComponent<SlimeMovement>();
        _knockback = GetComponent<KnockbackController>();
        _currentHp = _data.MaxHp;
    }
    new protected void Start()
    {
        base.Start();
    }
    public void SetGrabbed(GrabController grabController)
    {
        _knockback.StopKnockback();
        ChangeState(new SlimeStates.GrabbedState(this));
    }
    public void OnReleasedAtGround()
    {
        ChangeState(new SlimeStates.GrabbableState(this));
    }
    public void OnHittedByPlayer(PlayerBehaviour player, int damage)
    {
        EffectManager.InstantiateHitEffect(transform.position);
        Vector3 impactPosition = transform.position + (player.transform.position - transform.position) / 2;
        _knockback.ApplyKnockback(impactPosition, Defs.KnockBackDistance.Small, Defs.KnockBackSpeed.Small);
        ChangeState(new SlimeStates.HittedState(this));
        TakeDamage(damage);
    }
    public void OnHittedByBullet(Vector3 landPosition, float damage)
    {
        EffectManager.InstantiateHitEffect(transform.position);
        Vector3 impactPosition = transform.position + (landPosition - transform.position) / 2;
        _knockback.ApplyKnockback(impactPosition, Defs.KnockBackDistance.Big, Defs.KnockBackSpeed.Small);
        ChangeState(new SlimeStates.HittedState(this));
        TakeDamage(damage);
    }

    public void TakeDamage(float damage)
    {
        _currentHp -= damage;
        if(_currentHp <= 0)
        {
            if(_data.Type == SlimeType.Bullet)
                ChangeState(new SlimeStates.GrabbableState(this));
            else
                ChangeState(new SlimeStates.DeadState(this));
        }
    }

    protected override StateBase GetInitialState()
    {
        return new SlimeStates.IdleState(this);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (!IsAlive)
            return;
        var player = collision.collider.GetComponent<IAttackableBySlime>();
        if (player != null)
        {
            player.OnHittedBySlime(this, AttackPower);
        }
    }
}
