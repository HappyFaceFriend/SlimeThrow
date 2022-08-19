using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMoveState : PlayerState
{
    public PlayerMoveState(PlayerBehaviour player) : base("Move", player) { }

    public override void OnEnter()
    {
    }
    public override void OnUpdate()
    {
        if (!Player.Inputs.IsMovePressed)
            Player.ChangeState(new PlayerIdleState(Player));
        else if (Player.Inputs.IsDashPressed)
            Player.ChangeState(new PlayerDashState(Player));
        else if (Player.Inputs.IsGrabPressed)
        {
            if(Player.GrabController.GrabSlime() == GrabResult.Success)
                Player.ChangeState(new GrabMoveState(Player));
        }
        Player.transform.position += Player.Inputs.MoveInput * Player.MoveSpeed * Time.deltaTime;
    }
}
