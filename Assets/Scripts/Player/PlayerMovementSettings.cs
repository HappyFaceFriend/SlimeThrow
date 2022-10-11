using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Settings/PlayerMovement")]
public class PlayerMovementSettings : ScriptableObject
{
    [Header("Movement Settings")]
    [SerializeField] float _speedUpTime;
    [SerializeField] float _slowDownTime;
    [SerializeField] float _moveSpeed;
    [Header("Dash Settings")]
    [SerializeField] float _dashDistance;
    [SerializeField] float _dashDuration;
    [Tooltip("x : 시간 (0~1), y : 총 이동 거리")]
    [SerializeField] AnimationCurve _dashCurve;

    public float SpeedUpTime { get { return _speedUpTime; } }
    public float SlowDownTime { get { return _slowDownTime; } }
    public float MoveSpeed { get { return _moveSpeed; } }
    public float DashDistance { get { return _dashDistance; } }
    public float DashDuration { get { return _dashDuration; } }
    public AnimationCurve DashCurve { get { return _dashCurve; } }
}
