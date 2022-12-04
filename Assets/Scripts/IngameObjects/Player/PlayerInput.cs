using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInput : MonoBehaviour
{
    private Vector3 _moveInput;
    private Vector3 _lastMoveInput;
    public Vector3 MoveInput { get { return _moveInput; } }
    public Vector3 LastMoveInput { get { return _lastMoveInput; } }

    PlayerBehaviour _player;

    public bool IsMovePressed { get { return _moveInput != Vector3.zero && !_player.EverythingStopped; } }
    public bool IsDashPressed { get { return Input.GetKeyDown(KeyCode.Space) && !_player.EverythingStopped; } }
    public bool IsReleasePressed { get { return Input.GetMouseButtonDown(0) && !_player.EverythingStopped; } }
    public bool IsAttackPressed { get { return Input.GetMouseButtonDown(0) && !_player.EverythingStopped; } }
    public bool IsGetInTurretPressed { get { return Input.GetKeyDown(KeyCode.E) && !_player.EverythingStopped; } }
    public bool IsGrabFlowerPressed { get { return Input.GetKeyDown(KeyCode.F) && !_player.EverythingStopped; } }
    public bool IsReleaseFlowerPressed { get { return Input.GetMouseButtonDown(0) && !_player.EverythingStopped; } }
    private void Awake()
    {
        _moveInput = Vector3.zero;
        _player = GetComponent<PlayerBehaviour>();
        _lastMoveInput = new Vector3(1,0,0);
    }
    private void Update()
    {
        UpdateMoveInput();
        if (_moveInput != Vector3.zero)
            _lastMoveInput = _moveInput;

    }
    void UpdateMoveInput()
    {
        _moveInput = Vector3.zero;
        if (Input.GetKey(KeyCode.W))
            _moveInput.y += 1;
        if (Input.GetKey(KeyCode.S))
            _moveInput.y -= 1;
        if (Input.GetKey(KeyCode.A))
            _moveInput.x -= 1;
        if (Input.GetKey(KeyCode.D))
            _moveInput.x += 1;

        _moveInput.Normalize();
    }
}
