using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerBehaviour : StateMachineBase
{
    [SerializeField] private float _moveSpeed;
    [SerializeField] private float _dashDistance;
    [SerializeField] private float _dashDuration;
    [Tooltip("x : �ð� (0~1), y : �� �̵� �Ÿ�")]
    [SerializeField] private AnimationCurve _dashCurve;
    public float MoveSpeed { get { return _moveSpeed; } }
    public float DashDistance { get { return _dashDistance; } }
    public float DashDuration { get { return _dashDuration; } }
    public AnimationCurve DashCurve { get { return _dashCurve; } }
    public PlayerInput Inputs { get { return _inputs; } }

    private PlayerInput _inputs;

    private void Awake()
    {
        _inputs = GetComponent<PlayerInput>();
    }

    protected override StateBase GetInitialState()
    {
        return new PlayerIdleState(this);
    }
    
}
