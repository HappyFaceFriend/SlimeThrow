using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovementController : MonoBehaviour
{
    PlayerBehaviour _player;
    float _currentSpeed = 0f;
    private void Awake()
    {
        _player = GetComponent<PlayerBehaviour>();
    }
    public void MoveByInput()
    {
        if (_player.Inputs.IsMovePressed)
        {
            _currentSpeed += _player.MoveSpeed * (Time.deltaTime / _player.SpeedUpTime);
            if(_currentSpeed > _player.MoveSpeed)
                _currentSpeed = _player.MoveSpeed;
        }
        else
        {
            _currentSpeed -= _player.MoveSpeed * (Time.deltaTime / _player.SlowDownTime);
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
