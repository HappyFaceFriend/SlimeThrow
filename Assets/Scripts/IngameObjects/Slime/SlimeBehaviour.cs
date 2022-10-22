using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SlimeBehaviour : StateMachineBase, IGrababble
{
    [SerializeField] SlimeData _data;
    [SerializeField] float _grabbableDuration;
    [SerializeField] FlipObjectToPoint _flip;

    KnockbackController _knockback;
    public bool IsGrabbable { get { return CurrentState is SlimeStates.GrabbableState; } }
    public bool IsAlive { get { return !(CurrentState is SlimeStates.DeadState || CurrentState is SlimeStates.GrabbableState ||
                                        CurrentState is SlimeStates.GrabbedState); } }
    public float GrabbableDuration { get { return _grabbableDuration; } }
    public Sprite SlotIcon { get { return _data.SlotIcon; } }

    public SlimeBulletEffect BulletEffect { get; private set; }
    public FlipObjectToPoint Flipper { get { return _flip; } }

    HpSystem _hpSystem;
    BuffManager<SlimeBehaviour> _buffManager = new BuffManager<SlimeBehaviour>();
    [SerializeField] FlashWhenHitted _flasher;
    public BuffableStat MoveSpeed { get; private set; }
    public BuffableStat DamageAsBullet { get; private set; }
    public BuffableStat AttackRange { get; private set; }
    public BuffableStat AttackPower { get; private set; }
    public BuffableStat AttackSpeed { get; private set; }
    public BuffableStat SightRange { get; private set; }

    private void Awake()
    {
        BulletEffect = GetComponent<SlimeBulletEffect>();
        _knockback = GetComponent<KnockbackController>();

        _hpSystem = new HpSystem(_data.MaxHp, OnDie);
        AttackSpeed = new BuffableStat(_data.AttackSpeed);
        AttackPower = new BuffableStat(_data.AttackPower);
        MoveSpeed = new BuffableStat(_data.MoveSpeed);
        DamageAsBullet = new BuffableStat(_data.DamageAsBullet);
        AttackRange = new BuffableStat(_data.AttackRange);
        SightRange = new BuffableStat(_data.SightRange);
    }
    private void Update()
    {
        base.Update();
        _buffManager.OnUpdate();
    }
    public void ApplyBuff(Buff<SlimeBehaviour> buff)
    {
        buff.SetOwner(this);
        _buffManager.AddBuff(buff);
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
    public void OnHittedByPlayer(PlayerBehaviour player, float damage)
    {
        EffectManager.InstantiateHitEffect(transform.position);
        Vector3 impactPosition = transform.position + (player.transform.position - transform.position) / 2;
        _knockback.ApplyKnockback(impactPosition, Defs.KnockBackDistance.Small, Defs.KnockBackSpeed.Small);
        ChangeState(new SlimeStates.HittedState(this));
        _hpSystem.ChangeHp(-damage);
        _flasher.Flash(0.1f);
    }
    public void OnHittedByBullet(Vector3 landPosition, float damage)
    {
        EffectManager.InstantiateHitEffect(transform.position);
        Vector3 impactPosition = transform.position + (landPosition - transform.position) / 2;
        _knockback.ApplyKnockback(impactPosition, Defs.KnockBackDistance.Big, Defs.KnockBackSpeed.Small);
        ChangeState(new SlimeStates.HittedState(this));
        _hpSystem.ChangeHp(-damage);
        _flasher.Flash(0.1f);
    }
    public void TakeDamage(float damage)
    {
        _hpSystem.ChangeHp(-damage);
    }
    void OnDie()
    {
        if (_data.Type == SlimeType.Bullet)
            ChangeState(new SlimeStates.GrabbableState(this));
        else
            ChangeState(new SlimeStates.DeadState(this));
    }

    protected override StateBase GetInitialState()
    {
        return new SlimeStates.IdleState(this);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (!IsAlive)
            return;
        var target = collision.collider.GetComponent<IAttackableBySlime>();
        if (target != null)
        {
            target.OnHittedBySlime(this, AttackPower.Value);
        }
    }
}
