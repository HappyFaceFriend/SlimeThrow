using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace PlayerStates
{
    public class AttackingState : PlayerState
    {
        AttackController _attackController;
        MovementController _movement;
        public AttackingState(PlayerBehaviour player) : base("Attack", player) { }
        public override void OnEnter()
        {
            base.OnEnter();
            _movement = Player.GetComponent<MovementController>();
            _attackController = Player.GetComponent<AttackController>();
            _attackController.Attack();
            SetAnimState();
        }
        public override void OnUpdate()
        {
            _movement.MoveByInput();

            if (_attackController.IsAnimDone)
                Player.ChangeState(new DefaultState(Player));

        }
    }

}