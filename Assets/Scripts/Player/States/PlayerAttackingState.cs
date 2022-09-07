using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAttackingState : PlayerState
{
    AttackController _attackController;
    public PlayerAttackingState(PlayerBehaviour player) : base("Attack", player) { }
    public override void OnEnter()
    {
        _attackController = Player.GetComponent<AttackController>();
        _attackController.Attack();
    }
    public override void OnUpdate()
    {
        MoveByInput();

        if (_attackController.IsAnimDone)
            Player.ChangeState(new PlayerDefaultState(Player));

    }
}
