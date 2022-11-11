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
    public bool IsInvincible { get; set; }
    public bool IsTargetable { get; private set; }
    public BuffableStat GetInTurretRange { get; private set; }
    public BuffableStat MaxHp { get { return _hpSystem.MaxHp; } }
    public HpSystem HpSystem { get { return _hpSystem; } }
    public BuffableStat AttackPower { get; private set; }
    public BuffableStat AttackSpeed { get; private set; }
    public BuffableStat MoveSpeed { get; private set; }
    public BuffableStat PushToTowerRange { get; private set; }
    public PlayerInput Inputs { get { return _inputs; } }
    public bool IsAbleToAttack { get { return _attackController.IsAbleToAttack; } }
    public PlayerMovementSettings MovementSettings { get { return _movementSettings; } }

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
        _buffManager.AddBuff(buff);
    }

    new private void Update()
    {
        base.Update();
        _flip.TargetPoint = Utils.Inputs.GetMouseWordPos();
        _buffManager.OnUpdate();
        MoveInBounds();
    }
    public void LandWithBullet(Vector3 landPosition)
    {
        transform.position = landPosition;
        ChangeState(new PlayerStates.DefaultState(this));
        IsInvincible = false;
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
        if (IsInvincible)
            return;
        Vector3 impactPosition = transform.position + (slime.transform.position - transform.position) / 2;
        _knockback.ApplyKnockback(impactPosition, Defs.KnockBackDistance.PlayerHitted, Defs.KnockBackSpeed.PlayerHitted);
        EffectManager.InstantiateHitEffect(transform.position);
        TakeDamage(damage);
    }

    public void OnHittedByPad(SlimeBehaviour slime, float damage)
    {
        if (IsInvincible)
            return;
        EffectManager.InstantiateHitEffect(transform.position);
        _hpSystem.ChangeHp(damage);
    }
    public void TakeDamage(float damage)
    {
        _hpSystem.ChangeHp(-damage);
        _hpBar.SetHp((int)_hpSystem.CurrentHp, (int)_hpSystem.MaxHp.Value);
    }
    void OnDie()
    {
        _flip.enabled = false;
        Destroy(GetComponent<CircleCollider2D>());
        ChangeState(new PlayerStates.DeadState(this));
        _levelManager.OnPlayerDead();
    }

}
