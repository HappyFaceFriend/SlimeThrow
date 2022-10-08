using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SlimeStates
{
    public class MoveState : SlimeState
    {
        SlimeMovement _movement;
        SlimeAttackBase _attack;
        public MoveState(SlimeBehaviour slime) : base("Move", slime) { }

        public override void OnEnter()
        {
            Slime.Flipper.enabled = true;
            _movement = Slime.GetComponent<SlimeMovement>();
            _attack = Slime.GetComponent<SlimeAttackBase>();
            _movement.OnStartMoving();
            SetAnimState();
        }

        public override void OnUpdate()
        {
            Slime.Flipper.targetPoint = _movement.TargetPos;
            _movement.OnUpdate();
            if (_movement.IsMovementDone)
                Slime.ChangeState(new IdleState(Slime));

            if(_attack != null)
            {
                Transform attackTarget = _attack.GetAttackableTarget();
                if (attackTarget != null && _attack.IsCoolDownReady)
                {
                    Slime.ChangeState(new AttackState(Slime, attackTarget));
                }
            }

        }
    }
}
