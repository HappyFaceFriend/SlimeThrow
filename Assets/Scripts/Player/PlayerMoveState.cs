using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMoveState : StateBase
{
    Vector2 _moveDir;
    PlayerBehaviour _player;
    public PlayerMoveState(PlayerBehaviour player) : base("Move", player)
    {
        _player = player;
    }

    public override void OnEnter()
    {
        _moveDir = new Vector2(0, 0);
    }
    public override void OnUpdate()
    {
        UpdateMoveDir();

        if (_moveDir.Equals(Vector2.zero))
            _player.ChangeState(new PlayerIdleState(_player));

        _player.transform.position += Utils.Vectors.Vec2ToVec3(_moveDir * _player.MoveSpeed * Time.deltaTime);
    }

    void UpdateMoveDir()
    {
        _moveDir = Vector2.zero;
        if (Input.GetKey(KeyCode.W))
            _moveDir.y += 1;
        if (Input.GetKey(KeyCode.S))
            _moveDir.y -= 1;
        if (Input.GetKey(KeyCode.A))
            _moveDir.x -= 1;
        if (Input.GetKey(KeyCode.D))
            _moveDir.x += 1;
    }
}
