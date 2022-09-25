using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAttackingState : PlayerState
{
    AttackController _attackController;
    MovementController _movement;
    public PlayerAttackingState(PlayerBehaviour player) : base("Attack", player) { }
    public override void OnEnter()
    {
        base.OnEnter();
        _movement = Player.GetComponent<MovementController>();
        _attackController = Player.GetComponent<AttackController>();
        _attackController.Attack();
        SetAnimState(Player.Animator);
    }
    public override void OnUpdate()
    {
        _movement.MoveByInput();

        if (_attackController.IsAnimDone)
            Player.ChangeState(new PlayerDefaultState(Player));

    }
}
