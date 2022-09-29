using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerGrabbingState : PlayerState
{
    MovementController _movement;
    public PlayerGrabbingState(PlayerBehaviour player) : base("Grab", player) { }
    public override void OnEnter()
    {
        base.OnEnter();
        _movement = Player.GetComponent<MovementController>();
        SetAnimState();
    }


    public override void OnUpdate()
    {
        _movement.MoveByInput();
        _movement.CheckSpeed();

        if (Player.Inputs.IsReleasePressed)
        {
            Player.GrabController.ReleaseSlime();
            Player.ChangeState(new PlayerDefaultState(Player));
        }
    }
}
