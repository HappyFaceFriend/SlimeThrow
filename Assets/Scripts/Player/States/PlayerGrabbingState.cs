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
        Player.Animator.SetBool("onGrab", true);
    }


    public override void OnUpdate()
    {
        _movement.MoveByInput();
        _movement.CheckSpeed();

        if (Player.Inputs.IsReleasePressed)
        {
            Player.Animator.SetBool("onGrab", false);
            Player.GrabController.ReleaseSlime();
            Player.ChangeState(new PlayerDefaultState(Player));
        }
    }
}
