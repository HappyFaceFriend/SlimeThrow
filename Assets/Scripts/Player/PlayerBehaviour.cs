using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerBehaviour : StateMachineBase, IAttackableBySlime
{ 
    public bool IsInvincible { get; set; }
    public bool IsTargetable { get; private set; }
    [SerializeField] FlipObjectToPoint _flip;
    TurretBehaviour _turret;
    [SerializeField] float _getInTurretRange;
    [SerializeField] PlayerMovementSettings _movementSettings;
    [SerializeField] PlayerCombatSettings _combatSettings;
    KnockbackController _knockback;
    public GrabController GrabController { get { return _grabController; } }

    public float GetInTurretRange { get { return _getInTurretRange; } }
    public float AttackPower { get { return _combatSettings.AttackPower; } }
    public float AttackSpeed { get { return _combatSettings.AttackSpeed; } }
    public float AttackCoolTime { get { return 1f / _combatSettings.AttackSpeed; } }
    public PlayerInput Inputs { get { return _inputs; } }
    public TurretBehaviour Turret { get { return _turret; } }
    public bool IsAbleToAttack { get { return _attackController.IsAbleToAttack; } }
    public PlayerMovementSettings MovementSettings { get { return _movementSettings; } }

    public Sprite SlotIcon { get { return _combatSettings.SlotIcon; } }
    private PlayerInput _inputs;
    private GrabController _grabController;
    AttackController _attackController;


    float _currentHp;
    private void Awake()
    {
        _inputs = GetComponent<PlayerInput>();
        _grabController = GetComponent<GrabController>();
        _knockback = GetComponent<KnockbackController>();
        _attackController = GetComponent<AttackController>();
        _turret = GlobalRefs.Turret;
        IsInvincible = false;
        IsTargetable = true;
        _currentHp = _combatSettings.MaxHp;
    }

    new private void Update()
    {
        base.Update();
        _flip.TargetPoint = Utils.Inputs.GetMouseWordPos();
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

    public void TakeDamage(float damage)
    {
        _currentHp -= damage;
        if (_currentHp <= 0)
        {
            _flip.enabled = false;
            Destroy(GetComponent<CircleCollider2D>());
            ChangeState(new PlayerStates.DeadState(this));
        }
    }

}
