using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace PlayerStates
{
    public class GrabbingState : PlayerState
    {
        MovementController _movement;
        public GrabbingState(PlayerBehaviour player) : base("Grab", player) { }
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
                Player.ChangeState(new DefaultState(Player));
            }
        }
    }

}