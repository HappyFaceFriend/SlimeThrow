using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GrabMoveState : PlayerState
{
    public GrabMoveState(PlayerBehaviour player) : base("GrabMove", player) { }


    public override void OnUpdate()
    {
        Player.transform.position += Player.Inputs.MoveInput * Player.MoveSpeed * Time.deltaTime;
        if (!Player.Inputs.IsMovePressed)
            Player.ChangeState(new GrabIdleState(Player));
        if(Player.Inputs.IsThrowPressed)
        {
            Player.GrabController.ThrowSlime();
            Player.ChangeState(new PlayerMoveState(Player));
        }    
    }
}
