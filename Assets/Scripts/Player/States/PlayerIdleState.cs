using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerIdleState : PlayerState
{
    public PlayerIdleState(PlayerBehaviour player) : base("Idle", player) { }
    public override void OnUpdate()
    {
        if (Player.Inputs.IsMovePressed)
            Player.ChangeState(new PlayerMoveState(Player));
        else if (Player.Inputs.IsDashPressed)
            Player.ChangeState(new PlayerDashState(Player));
    }
}
