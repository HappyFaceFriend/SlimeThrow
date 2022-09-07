using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerDefaultState : PlayerState
{
    public PlayerDefaultState(PlayerBehaviour player) : base("Default", player) { }
    public override void OnUpdate()
    {
        if (Player.Inputs.IsDashPressed)
            Player.ChangeState(new PlayerDashState(Player));
        else if (Player.Inputs.IsGrabPressed)
        {
            if (Player.GrabController.GrabSlime() == GrabResult.Success)
                Player.ChangeState(new PlayerGrabbingState(Player));
        }
        else if (Player.Inputs.IsAttackPressed)
        {
            Player.ChangeState(new PlayerAttackingState(Player));
        }

        MoveByInput();

    }
}
