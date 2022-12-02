using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SlimeBehaviour : StateMachineBase
{
    [SerializeField] SlimeData _data;
    [SerializeField] float _grabbableDuration;
    [SerializeField] FlipObjectToPoint _flip;

    protected KnockbackController _knockback;
    public bool IsOnFire { get { return _buffManager.HasBuff(typeof(SlimeBuffs.Burn)); }  }
    public bool CriticalOn { get; set; } = false;
    public bool FireBallOn { get; set; } = false;
    public bool IsGrabbable { get; set; } = false;
    public bool FireSlayerOn { get; set; } = false;
    public bool PuttedInTurret { get; set; } = false;


    public bool IsAlive
    {
        get
        {
            return !(CurrentState is SlimeStates.DeadState || CurrentState is SlimeStates.GrabbableState ||
                                        CurrentState is SlimeStates.GrabbedState);
        }
    }
    public bool IsFever()
    {
        if(_hpSystem.CurrentHp <= _hpSystem.MaxHp.Value/2)
            return true;
        else
            return false;
    }

    public bool FlameBullet { get; set; } = false;
    public bool BurningFist { get; set; } = false;
    

    public float GrabbableDuration { get { return _grabbableDuration; } }
    public Sprite SlotIcon { get { return _data.SlotIcon; } }

    public SlimeBulletEffect BulletEffect { get; private set; }
    public FlipObjectToPoint Flipper { get { return _flip; } }

    HpSystem _hpSystem;

    BuffManager<SlimeBehaviour> _buffManager = new BuffManager<SlimeBehaviour>();
    [SerializeField] FlashWhenHitted _flasher;
    [SerializeField] SquashWhenHitted _squasher;
    [SerializeField] float _normPosOfFlowerAttack;
    [SerializeField] bool _isGrabbableAtDeath = false;
    public float NormPosOfFlowerAttack { get { return _normPosOfFlowerAttack; } }
    public HpSystem HPSystem { get { return _hpSystem; } }
    public SlimeData Data { get { return _data; } }
    public BuffableStat MoveSpeed { get; private set; }
    public BuffableStat DamageAsBullet { get; private set; }
    public BuffableStat AttackRange { get; private set; }
    public BuffableStat AttackPower { get; private set; }
    public BuffableStat FlowerAttackPower { get; private set; }
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
        FlowerAttackPower = new BuffableStat(_data.FlowerAttackPower);
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
        if (BurningFist)
            ApplyBuff(new SlimeBuffs.Burn(GlobalRefs.EffectStatManager._burn.Duration.Value, 2, 0.5f));
        Vector3 impactPosition = transform.position + (player.transform.position - transform.position) / 2;
        _knockback.ApplyKnockback(impactPosition, Defs.KnockBackDistance.Small, Defs.KnockBackSpeed.Small);
        if (IsOnFire & CriticalOn)
            damage *= 1.2f;
        OnGetHitted(impactPosition, damage, false);
    }
    public void OnHittedByBullet(Vector3 landPosition, float damage)
    {
        Vector3 impactPosition = transform.position + (landPosition - transform.position) / 2;
        _knockback.ApplyKnockback(impactPosition, 4, Defs.KnockBackSpeed.Small);
        if (_hpSystem.CurrentHp < (_hpSystem.MaxHp.Value / 2) & FireSlayerOn & GlobalRefs.Turret._bulletBuilder._slimeName == "Fire Slime")
            OnGetHitted(impactPosition, 999f, true);
        else
            OnGetHitted(impactPosition, damage, false);

        if (!IsOnFire & FireBallOn)
            ApplyBuff(new SlimeBuffs.Burn(GlobalRefs.EffectStatManager._burn.Duration.Value, GlobalRefs.EffectStatManager._burn.DamagePerTick.Value, 0.5f));
    }
    protected void OnGetHitted(Vector3 impactPosition, float damage, bool slay)
    {
        EffectManager.InstantiateHitEffect(transform.position);
        _squasher.Squash();
        TakeDamage(damage);
        if (_hpSystem.IsDead)
        {
            _camera.Shake(CameraController.ShakePower.SlimeLastHitted);
            SoundManager.Instance.PlaySFX("SlimeLastHitted");
            //ChangeState(new SlimeStates.GrabbableState(this));
        }
        else if(CurrentState is SlimeStates.FreezeState) // 안 죽었고 얼어 있는 상태면 맞아도 가만히 
        {
            _camera.Shake(CameraController.ShakePower.SlimeHitted);
            //ChangeState(new SlimeStates.HittedState(this));
        }
        else
        {
            _camera.Shake(CameraController.ShakePower.SlimeHitted);
            if(Random.Range(0f,1f) > 0.5f)
                SoundManager.Instance.PlaySFX("SlimeHitted1");
            else
                SoundManager.Instance.PlaySFX("SlimeHitted2");
            ChangeState(new SlimeStates.HittedState(this));
        }
    }
    public void TakeDamage(float damage) // 이거를 플레이어한테 달아주면 된다
    {
        _flasher.Flash();
        if(damage == 999f)
            EffectManager.InstantiateDamageTextEffect(transform.position, damage, DamageTextEffect.Type.SlimeSlayed);
        else
            EffectManager.InstantiateDamageTextEffect(transform.position, damage, DamageTextEffect.Type.SlimeHitted);
        _hpSystem.ChangeHp(-damage);
        if (Random.Range(0f, 1f) > 0.5f)
            SoundManager.Instance.PlaySFX("SlimeHitted1", 0.15f);
        else
            SoundManager.Instance.PlaySFX("SlimeHitted2", 0.15f);
    }
    void OnDie()
    {
        if(_isGrabbableAtDeath)
        {
            ChangeState(new SlimeStates.GrabbableState(this));
            return;
        }
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
        float smokeSpeed = 2f;
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
            if(collision.transform == GlobalRefs.Player.transform)
                target.OnHittedBySlime(this, AttackPower.Value);
            else if(collision.transform == GlobalRefs.Flower.transform)
                target.OnHittedBySlime(this, FlowerAttackPower.Value);

        }
    }
}
