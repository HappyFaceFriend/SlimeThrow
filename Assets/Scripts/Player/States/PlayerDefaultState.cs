using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerDefaultState : PlayerState
{
    MovementController _movement;
    public PlayerDefaultState(PlayerBehaviour player) : base("Default", player) { }
    public override void OnEnter()
    {
        base.OnEnter();
        _movement = Player.GetComponent<MovementController>();
        Player.Animator.SetTrigger("Default");
    }
    public override void OnUpdate()
    {
        if (Player.Inputs.IsDashPressed)
            Player.ChangeState(new PlayerDashState(Player));
        else if (Player.Inputs.IsAttackPressed)
        {
            Player.ChangeState(new PlayerAttackingState(Player));
        }

        _movement.MoveByInput();
        _movement.CheckSpeed();

        if (Player.GrabController.GrabSlime() == GrabResult.Success)
            Player.ChangeState(new PlayerGrabbingState(Player));
    }
}
