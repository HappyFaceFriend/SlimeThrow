using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace PlayerStates
{
    public class DeadState : PlayerState
    {
        public DeadState(PlayerBehaviour player) : base("Dead", player) 
        {

        }

        public override void OnEnter()
        {
            base.OnEnter();
            SetAnimState();
        }

    }

}

