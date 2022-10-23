using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovementController : MonoBehaviour
{
    [SerializeField] float _dustIntervalDistance;
    float _dustMovedDistance = 0f;
    PlayerBehaviour _player;
    PlayerMovementSettings _settings;
    bool _isMovePressedLastFrame = false;
    float _currentSpeed = 0f;
    private void Awake()
    {
        _player = GetComponent<PlayerBehaviour>();
        _settings = _player.MovementSettings;
    }
    public void MoveByInput()
    {
        if (_player.Inputs.IsMovePressed)
        {
            _currentSpeed += _player.MoveSpeed.Value * (Time.deltaTime / _settings.SpeedUpTime);
            if(_currentSpeed > _player.MoveSpeed.Value)
                _currentSpeed = _player.MoveSpeed.Value;
        }
        else
        {
            _currentSpeed -= _player.MoveSpeed.Value * (Time.deltaTime / _settings.SlowDownTime);
            if(_currentSpeed < 0)
                _currentSpeed = 0;
        }
        Vector3 moveVec = _player.Inputs.LastMoveInput * _currentSpeed * Time.deltaTime;
        transform.position += moveVec;
        _dustMovedDistance += moveVec.magnitude;
        if(_dustMovedDistance >= _dustIntervalDistance)
        {
            _dustMovedDistance -= _dustIntervalDistance;
            EffectManager.InstantiateDustEffect(transform.position);
        }
    }
    private void Update()
    {
        if (_isMovePressedLastFrame == false && _player.Inputs.IsMovePressed == true)
        {
            _dustMovedDistance = _dustIntervalDistance;
        }
        if (_player.Inputs.IsMovePressed)
            _isMovePressedLastFrame = true;
        else
            _isMovePressedLastFrame = false;

    }
    public void CheckSpeed()
    {
        if (_currentSpeed > 0)
            _player.Animator.SetBool("IsMoving", true);
        else

            _player.Animator.SetBool("IsMoving", false);
    }
}
