using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerBehaviour : StateMachineBase, IAttackableBySlime
{
    [SerializeField] FlipObjectToPoint _flip;
    [SerializeField] PlayerMovementSettings _movementSettings;
    [SerializeField] PlayerCombatSettings _combatSettings;
    [SerializeField] HpBar _hpBar;
    [SerializeField] LevelManager _levelManager;
    public bool IsInvincible { get; private set; }
    public bool IsTargetable { get; private set; }
    public BuffableStat GetInTurretRange { get; private set; }
    public BuffableStat MaxHp { get { return _hpSystem.MaxHp; } }
    public HpSystem HpSystem { get { return _hpSystem; } }
    public BuffableStat AttackPower { get; private set; }
    public BuffableStat AttackSpeed { get; private set; }
    public BuffableStat MoveSpeed { get; private set; }
    public BuffableStat PushToTowerRange { get; private set; }
    public BuffableStat DamageAsBullet { get; private set; }
    public PlayerInput Inputs { get { return _inputs; } }
    public bool IsAbleToAttack { get { return _attackController.IsAbleToAttack; } }
    public bool EverythingStopped { get; set; }
    public PlayerMovementSettings MovementSettings { get { return _movementSettings; } }
    public Color Color { get { return _combatSettings.Color; } }
    public HpBar PlayerHPBar { get { return _hpBar; } }
    Utils.Timer _invincibleTimer;

    public bool UpgradeGetHP = false;
    public float _getHP = 0;

    public Sprite SlotIcon { get { return _combatSettings.SlotIcon; } }

    PlayerInput _inputs;
    AttackController _attackController;
    KnockbackController _knockback;

    HpSystem _hpSystem;
    BuffManager<PlayerBehaviour> _buffManager = new BuffManager<PlayerBehaviour>();
    private void Awake()
    {
        _inputs = GetComponent<PlayerInput>();
        _knockback = GetComponent<KnockbackController>();
        _attackController = GetComponent<AttackController>();
        IsInvincible = false;
        IsTargetable = true;
        _hpSystem = new HpSystem(_combatSettings.MaxHp, OnDie);
        _hpBar.SetHp((int)_hpSystem.CurrentHp, (int)_hpSystem.MaxHp.Value);
        AttackSpeed = new BuffableStat(_combatSettings.AttackSpeed);
        AttackPower = new BuffableStat(_combatSettings.AttackPower);
        MoveSpeed = new BuffableStat(_movementSettings.MoveSpeed);
        PushToTowerRange = new BuffableStat(_combatSettings.PushToTowerRange);
        GetInTurretRange = new BuffableStat(_combatSettings.GetInTurretRange);
        DamageAsBullet = new BuffableStat(_combatSettings.DamageAsBullet);
        _invincibleTimer = new Utils.Timer(1f);
        _invincibleTimer.Off();
    }

    public void SetInvincible(bool invincible, float duration = -1)
    {
        IsInvincible = invincible;
        if(invincible)
        {
            if (duration > 0)
            {
                _invincibleTimer.On();
                _invincibleTimer.Reset(duration);
            }
            else
                _invincibleTimer.Off();
        }
    }
    void MoveInBounds()
    {
        if (transform.position.x < -_levelManager.MapSize.x / 2)
            transform.position = new Vector3(-_levelManager.MapSize.x / 2, transform.position.y, 0);
        if (transform.position.x > _levelManager.MapSize.x / 2)
            transform.position = new Vector3(_levelManager.MapSize.x / 2, transform.position.y, 0);
        if (transform.position.y < -_levelManager.MapSize.y / 2)
            transform.position = new Vector3(transform.position.x, -_levelManager.MapSize.y / 2, 0);
        if (transform.position.y > _levelManager.MapSize.y / 2)
            transform.position = new Vector3(transform.position.x, _levelManager.MapSize.y / 2, 0);
    }
    public void ApplyBuff(Buff<PlayerBehaviour> buff)
    {
        if (EverythingStopped)
            return;
        buff.SetOwner(this);
        _buffManager.AddBuff(buff);
    }

    public void ForceOutOfTurret()
    {
        if(CurrentState is PlayerStates.InTurretState)
        {
            GlobalRefs.Turret.RemovePlayer();
            LandWithBullet(new Vector3(1, 0, 0));
        }
    }
    new private void Update()
    {
        base.Update();
        _flip.TargetPoint = Utils.Inputs.GetMouseWordPos();
        if(!EverythingStopped)
            _buffManager.OnUpdate();
        if(IsInvincible && _invincibleTimer.IsOn)
        {
            _invincibleTimer.Tick();
            if(_invincibleTimer.IsOver)
            {
                IsInvincible = false;
            }
        }
        MoveInBounds();
    }
    public void LandWithBullet(Vector3 landPosition)
    {
        transform.position = landPosition;
        ChangeState(new PlayerStates.DefaultState(this));
        IsTargetable = true;
    }
    protected override StateBase GetInitialState()
    {
        return new PlayerStates.DefaultState(this);
    }
    public void OnEnterTurret()
    {
        IsInvincible = true;
        IsTargetable = false;
    }
    public void OnHittedBySlime(SlimeBehaviour slime, float damage)
    {
        if (EverythingStopped)
            return;
        if (IsInvincible) 
            return;
        Vector3 impactPosition = transform.position + (slime.transform.position - transform.position) / 2;
        _knockback.ApplyKnockback(impactPosition, Defs.KnockBackDistance.PlayerHitted, Defs.KnockBackSpeed.PlayerHitted);
        EffectManager.InstantiateHitEffect(transform.position);
        if (GlobalRefs.UpgradeManager.GetCount("화염방어막") != 0 )
            damage /= 2f;
        TakeDamage(damage);
        SoundManager.Instance.PlaySFX("PlayerHitted");
    }

    public void OnHitted(float damage, Vector3 impactPosition, bool knockback)
    {
        if (EverythingStopped)
            return;
        if (IsInvincible)
            return;
        if(knockback)
            _knockback.ApplyKnockback(impactPosition, Defs.KnockBackDistance.PlayerHitted, Defs.KnockBackSpeed.PlayerHitted);
        EffectManager.InstantiateHitEffect(transform.position);
        TakeDamage(damage);
        SoundManager.Instance.PlaySFX("PlayerHitted", 0.15f);
    }
    public void TakeDamage(float damage)
    {
        if (EverythingStopped)
            return;
        _hpSystem.ChangeHp(-damage);
        _hpBar.SetHp((int)_hpSystem.CurrentHp, (int)_hpSystem.MaxHp.Value);
        EffectManager.InstantiateDamageTextEffect(transform.position, damage, DamageTextEffect.Type.PlayerHitted);
    }
    public void RecoveHP(float amount)
    {
        _hpSystem.ChangeHp(amount);
        _hpBar.SetHp((int)_hpSystem.CurrentHp, (int)_hpSystem.MaxHp.Value);
        Debug.Log("체력 " + amount + "회복");
    }
    void OnDie()
    {
        _flip.enabled = false;
        Destroy(GetComponent<CircleCollider2D>());
        foreach (SpriteRenderer sprite in GetComponentsInChildren<SpriteRenderer>())
        {
            sprite.enabled = enabled;
        }
        ChangeState(new PlayerStates.DeadState(this));
        _levelManager.OnPlayerDead();
    }
    public void SetUpgrade(float num)
    {
        UpgradeGetHP = true;
        _getHP += num;
    }
}
