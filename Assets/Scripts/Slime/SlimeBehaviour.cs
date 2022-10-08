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
    public bool IsGrabbable { get { return CurrentState is SlimeStates.GrabbableState; } }
    public float GrabbableDuration { get { return _grabbableDuration; } }
    public Sprite SlotIcon { get { return _data.SlotIcon; } }

    public SlimeBulletEffect BulletEffect { get; private set; }
    public FlipObjectToPoint Flipper { get { return _flip; } }

    public float MoveSpeed { get { return _data.MoveSpeed; } }
    public float DamageAsBullet { get { return _data.DamageAsBullet; } }
    public float AttackRange { get { return _data.AttackRange; } }
    public float AttackPower { get { return _data.AttackPower; } }

    private void Awake()
    {
        BulletEffect = GetComponent<SlimeBulletEffect>();
        _movement = GetComponent<SlimeMovement>();
        _currentHp = _data.MaxHp;
    }
    new protected void Start()
    {
        base.Start();
    }
    public void SetGrabbed(GrabController grabController)
    {
        ChangeState(new SlimeStates.GrabbedState(this));
    }
    public void OnReleasedAtGround()
    {
        ChangeState(new SlimeStates.DeadState(this));
    }
    public void OnHitted(PlayerBehaviour player, int damage)
    {
        EffectManager.InstantiateHitEffect(transform.position);
        TakeDamage(damage);
    }
    public void OnHittedByBullet(float damage)
    {
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
}
