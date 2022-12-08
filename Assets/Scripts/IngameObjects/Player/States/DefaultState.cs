using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace PlayerStates
{
    public class DefaultState : PlayerState
    {
        MovementController _movement;
        GrabController _grabController;
        public DefaultState(PlayerBehaviour player) : base("Default", player) { }
        public override void OnEnter()
        {
            base.OnEnter();
            _movement = Player.GetComponent<MovementController>();
            _grabController = Player.GetComponent<GrabController>();
            SetAnimState();
        }
        public override void OnUpdate()
        {
            if (Player.Inputs.IsDashPressed && !Player.IsStunned)
                Player.ChangeState(new DashState(Player));
            else if (Player.Inputs.IsAttackPressed && Player.IsAbleToAttack)
            {
                    Player.ChangeState(new AttackingState(Player));
            }
            else if (Player.Inputs.IsGrabFlowerPressed && !Player.IsStunned)
            {
                if (_grabController.GrabFlower() == GrabResult.Success)
                    Player.ChangeState(new GrabbingState(Player));
            }
            if (Player.Inputs.IsGetInTurretPressed && !Player.IsStunned && Utils.Vectors.IsInDistance(transform.position, GlobalRefs.Turret.transform.position, Player.GetInTurretRange.Value))
                Player.ChangeState(new EnterTurretState(Player));
            
            _movement.MoveByInput();
            _movement.CheckSpeed();
            

            if (_grabController.GrabSlime() == GrabResult.Success)
                Player.ChangeState(new GrabbingState(Player));
        }

    }

}