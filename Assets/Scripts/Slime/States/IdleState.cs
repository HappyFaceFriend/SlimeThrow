using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SlimeStates
{
    public class IdleState : SlimeState
    {
        SlimeMovement _movement;
        public IdleState(SlimeBehaviour slime) : base("Idle", slime) { }

        public override void OnEnter()
        {
            base.OnEnter();
            Slime.Flipper.enabled = true;
            _movement = Slime.GetComponent<SlimeMovement>();
            SetAnimState();
        }
        public override void OnUpdate()
        {
            base.OnUpdate();
            Slime.Flipper.targetPoint = _movement.TargetPos;
        }
    }


}