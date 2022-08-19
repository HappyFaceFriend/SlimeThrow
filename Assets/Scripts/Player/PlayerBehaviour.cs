using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerBehaviour : StateMachineBase
{
    [SerializeField] private float _moveSpeed;
    [SerializeField] private float _dashDistance;
    [SerializeField] private float _dashDuration;
    [Tooltip("x : 시간 (0~1), y : 총 이동 거리")]
    [SerializeField] private AnimationCurve _dashCurve;
    public GrabController GrabController { get { return _grabController; } }

    public float MoveSpeed { get { return _moveSpeed; } }
    public float DashDistance { get { return _dashDistance; } }
    public float DashDuration { get { return _dashDuration; } }
    public AnimationCurve DashCurve { get { return _dashCurve; } }
    public PlayerInput Inputs { get { return _inputs; } }

    private PlayerInput _inputs;
    private GrabController _grabController;

    private void Awake()
    {
        _inputs = GetComponent<PlayerInput>();
        _grabController = GetComponent<GrabController>();
    }


    protected override StateBase GetInitialState()
    {
        return new PlayerIdleState(this);
    }
    
}
