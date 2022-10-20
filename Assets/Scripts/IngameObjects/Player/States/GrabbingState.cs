using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace PlayerStates
{
    public class GrabbingState : PlayerState
    {
        MovementController _movement;
        GrabController _grabController;
        public GrabbingState(PlayerBehaviour player) : base("Grab", player) { }
        public override void OnEnter()
        {
            base.OnEnter();
            _movement = Player.GetComponent<MovementController>();
            _grabController = Player.GetComponent<GrabController>();
            SetAnimState();
        }


        public override void OnUpdate()
        {
            _movement.MoveByInput();
            _movement.CheckSpeed();

            if (Player.Inputs.IsReleasePressed)
            {
                _grabController.Release();
                if(_grabController.GrabbedFlower == null)
                    Player.ChangeState(new DefaultState(Player));
            }
        }
    }

}