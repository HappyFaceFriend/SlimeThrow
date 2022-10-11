using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace PlayerStates
{
    public class DefaultState : PlayerState
    {
        MovementController _movement;
        public DefaultState(PlayerBehaviour player) : base("Default", player) { }
        public override void OnEnter()
        {
            base.OnEnter();
            _movement = Player.GetComponent<MovementController>();
            SetAnimState();
        }
        public override void OnUpdate()
        {
            if (Player.Inputs.IsDashPressed)
                Player.ChangeState(new DashState(Player));
            else if (Player.Inputs.IsAttackPressed && Player.IsAbleToAttack)
            {
                if (Player.GrabController.GrabFlower() == GrabResult.Success)
                    Player.ChangeState(new GrabbingState(Player));
                else
                    Player.ChangeState(new AttackingState(Player));
            }
            if (Player.Inputs.IsGetInTurretPressed && Utils.Vectors.IsInDistance(transform.position, Turret.transform.position, Player.GetInTurretRange))
                Player.ChangeState(new EnterTurretState(Player));
            
            _movement.MoveByInput();
            _movement.CheckSpeed();
            

            if (Player.GrabController.GrabSlime() == GrabResult.Success)
                Player.ChangeState(new GrabbingState(Player));
        }

    }

}