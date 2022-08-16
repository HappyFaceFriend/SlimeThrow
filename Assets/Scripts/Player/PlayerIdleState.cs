using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerIdleState : StateBase
{
    PlayerBehaviour _player;
    public PlayerIdleState(PlayerBehaviour player) : base("Idle", player) 
    {
        _player = player;
    }
    public override void OnUpdate()
    {
        if (Input.GetKey(KeyCode.W) || Input.GetKey(KeyCode.S) || Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.D))
            _player.ChangeState(new PlayerMoveState(_player));
    }
}
