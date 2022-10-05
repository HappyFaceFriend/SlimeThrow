using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerBehaviour : StateMachineBase
{ 
    [SerializeField] FlipObjectToPoint _flip;
    [SerializeField] TurretBehaviour _turret;
    [Header("Movement Settings")]
    [SerializeField] private float _speedUpTime;
    [SerializeField] private float _slowDownTime;
    [SerializeField] private float _moveSpeed;
    [Header("Dash Settings")]
    [SerializeField] private float _dashDistance;
    [SerializeField] private float _dashDuration;
    [Tooltip("x : 시간 (0~1), y : 총 이동 거리")]
    [SerializeField] private AnimationCurve _dashCurve;
    public GrabController GrabController { get { return _grabController; } }

    public float SpeedUpTime { get { return _speedUpTime; } }
    public float SlowDownTime { get { return _slowDownTime; } }
    public float MoveSpeed { get { return _moveSpeed; } }
    public float DashDistance { get { return _dashDistance; } }
    public float DashDuration { get { return _dashDuration; } }
    public AnimationCurve DashCurve { get { return _dashCurve; } }
    public PlayerInput Inputs { get { return _inputs; } }
    public TurretBehaviour Turret { get { return _turret; } }

    private PlayerInput _inputs;
    private GrabController _grabController;

    private void Awake()
    {
        _inputs = GetComponent<PlayerInput>();
        _grabController = GetComponent<GrabController>();
    }

    private void Update()
    {
        base.Update();
        _flip.targetPoint = Utils.Inputs.GetMouseWordPos();
        if (Input.GetKeyDown(KeyCode.E))
            ChangeState(new PlayerStates.EnterTurretState(this));
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
    
}
