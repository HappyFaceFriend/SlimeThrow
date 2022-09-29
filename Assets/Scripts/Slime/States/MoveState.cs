using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SlimeStates
{
    public class MoveState : SlimeState
    {
        SlimeMovement _movement;
        public MoveState(SlimeBehaviour slime) : base("Move", slime) { }

        public override void OnEnter()
        {
            Slime.Flipper.enabled = true;
            _movement = Slime.GetComponent<SlimeMovement>();
            _movement.OnStartMoving();
            SetAnimState();
        }

        public override void OnUpdate()
        {
            Slime.Flipper.targetPoint = _movement.TargetPos;
            _movement.OnUpdate();
            if (_movement.IsMovementDone)
                Slime.ChangeState(new IdleState(Slime));
        }
    }
}
