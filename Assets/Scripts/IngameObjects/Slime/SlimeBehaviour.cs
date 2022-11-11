using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SlimeBehaviour : StateMachineBase
{
    [SerializeField] SlimeData _data;
    [SerializeField] float _grabbableDuration;
    [SerializeField] FlipObjectToPoint _flip;

    protected KnockbackController _knockback;
    public bool IsGrabbable { get; set; } = false;
    public bool PuttedInTurret { get; set; } = false;
    public bool IsAlive { get { return !(CurrentState is SlimeStates.DeadState || CurrentState is SlimeStates.GrabbableState ||
                                        CurrentState is SlimeStates.GrabbedState); } }
    public float GrabbableDuration { get { return _grabbableDuration; } }
    public Sprite SlotIcon { get { return _data.SlotIcon; } }

    public SlimeBulletEffect BulletEffect { get; private set; }
    public FlipObjectToPoint Flipper { get { return _flip; } }

    HpSystem _hpSystem;
    BuffManager<SlimeBehaviour> _buffManager = new BuffManager<SlimeBehaviour>();
    [SerializeField] FlashWhenHitted _flasher;
    [SerializeField] SquashWhenHitted _squasher;
    public BuffableStat MoveSpeed { get; private set; }
    public BuffableStat DamageAsBullet { get; private set; }
    public BuffableStat AttackRange { get; private set; }
    public BuffableStat AttackPower { get; private set; }
    public BuffableStat AttackSpeed { get; private set; }
    public BuffableStat SightRange { get; private set; }

    protected CameraController _camera;

    protected virtual void Awake()
    {
        _camera = Camera.main.GetComponent<CameraController>();
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
    public void OnReleasedAtGround(Vector3 direction, float range)
    {
        _knockback.ApplyKnockbackDir(direction, range, 7);
        ChangeState(new SlimeStates.GrabbableState(this, true));
    }
    public void OnHittedByPlayer(PlayerBehaviour player, float damage)
    {
        Vector3 impactPosition = transform.position + (player.transform.position - transform.position) / 2;
        _knockback.ApplyKnockback(impactPosition, Defs.KnockBackDistance.Small, Defs.KnockBackSpeed.Small);
        OnGetHitted(impactPosition, damage);
    }
    public void OnHittedByBullet(Vector3 landPosition, float damage)
    {
        Vector3 impactPosition = transform.position + (landPosition - transform.position) / 2;
        _knockback.ApplyKnockback(impactPosition, 4, Defs.KnockBackSpeed.Small);
        OnGetHitted(impactPosition, damage);
    }
    protected void OnGetHitted(Vector3 impactPosition, float damage)
    {
        EffectManager.InstantiateHitEffect(transform.position);
        _squasher.Squash();
        TakeDamage(damage);
        if(_hpSystem.IsDead)
        {
            _camera.Shake(CameraController.ShakePower.SlimeLastHitted);
            //ChangeState(new SlimeStates.GrabbableState(this));
        }
        else
        {
            _camera.Shake(CameraController.ShakePower.SlimeHitted);
            ChangeState(new SlimeStates.HittedState(this));
        }
    }
    public void TakeDamage(float damage) // 이거를 플레이어한테 달아주면 된다
    {
        _flasher.Flash(0.2f);
        EffectManager.InstantiateDamageTextEffect(Camera.main.WorldToScreenPoint(transform.position), damage);
        _hpSystem.ChangeHp(-damage);
    }
    void OnDie()
    {
        if(Random.Range(0f, 1f) <= GlobalRefs.UpgradeManager.GetGrabProbability(_data))
            ChangeState(new SlimeStates.GrabbableState(this));
        else
            ChangeState(new SlimeStates.DeadState(this));
    }

    private void OnDestroy()
    {
        if (PuttedInTurret)
            return;
        _camera.Shake(CameraController.ShakePower.SlimeHitted);
        float smokeSpeed = 0.7f;
        float angleOffset = Random.Range(-15, 15);
        EffectManager.InstantiateSmokeEffect(transform.position, Utils.Vectors.AngleToVector(90 + angleOffset) * smokeSpeed);
        EffectManager.InstantiateSmokeEffect(transform.position, Utils.Vectors.AngleToVector(225 + angleOffset) * smokeSpeed);
        EffectManager.InstantiateSmokeEffect(transform.position, Utils.Vectors.AngleToVector(315 + angleOffset) * smokeSpeed);
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
