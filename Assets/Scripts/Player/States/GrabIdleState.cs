using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GrabIdleState : PlayerState
{
    public GrabIdleState(PlayerBehaviour player) : base("GrabIdle", player) { }
    public override void OnUpdate()
    {
        if (Player.Inputs.IsMovePressed)
            Player.ChangeState(new GrabMoveState(Player));

        if (Player.Inputs.IsThrowPressed)
        {
            Player.GrabController.ThrowSlime();
            Player.ChangeState(new PlayerIdleState(Player));
        }
    }
}
