using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovementController : MonoBehaviour
{
    PlayerBehaviour _player;
    PlayerMovementSettings _settings;
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
        transform.position += _player.Inputs.LastMoveInput * _currentSpeed * Time.deltaTime;
    }
    public void CheckSpeed()
    {
        if (_currentSpeed > 0)
            _player.Animator.SetBool("IsMoving", true);
        else

            _player.Animator.SetBool("IsMoving", false);
    }
}
