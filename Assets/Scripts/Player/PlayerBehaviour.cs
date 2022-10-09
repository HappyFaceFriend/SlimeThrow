using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerBehaviour : StateMachineBase, IAttackableBySlime
{ 
    [SerializeField] FlipObjectToPoint _flip;
    TurretBehaviour _turret;
    [SerializeField] float _getInTurretRange;
    [SerializeField] PlayerMovementSettings _movementSettings;
    public GrabController GrabController { get { return _grabController; } }

    public float GetInTurretRange { get { return _getInTurretRange; } }
    public PlayerInput Inputs { get { return _inputs; } }
    public TurretBehaviour Turret { get { return _turret; } }

    public PlayerMovementSettings MovementSettings { get { return _movementSettings; } }

    private PlayerInput _inputs;
    private GrabController _grabController;


    float _currentHp;
    private void Awake()
    {
        _inputs = GetComponent<PlayerInput>();
        _grabController = GetComponent<GrabController>();
        _turret = GlobalRefs.Turret;
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
    }
    protected override StateBase GetInitialState()
    {
        return new PlayerStates.DefaultState(this);
    }
    public void OnHittedBySlime(SlimeBehaviour slime, float damage)
    {
        Debug.Log("Player hitted by " + slime.name);
        EffectManager.InstantiateHitEffect(transform.position);
        TakeDamage(damage);
    }

    public void TakeDamage(float damage)
    {
        _currentHp -= damage;
        if (_currentHp <= 0)
        {
        }
    }

}
