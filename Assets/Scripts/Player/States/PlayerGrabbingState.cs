using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerGrabbingState : PlayerState
{
    public PlayerGrabbingState(PlayerBehaviour player) : base("Grabbing", player) { }


    public override void OnUpdate()
    {
        MoveByInput();

        if(Player.Inputs.IsGrabPressed)
        {
            Player.GrabController.ReleaseSlime();
            Player.ChangeState(new PlayerDefaultState(Player));
        }
    }
}
