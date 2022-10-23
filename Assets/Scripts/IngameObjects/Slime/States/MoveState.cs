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
            SetAnimState();
        }

        public override void OnUpdate()
        {
            Slime.Flipper.TargetPoint = _movement.TargetPos;
            _movement.OnUpdate();

            if(_attack != null && _attack.IsCoolDownReady)
            {
                Transform attackTarget = _attack.GetAttackableTarget();
                if (attackTarget != null)
                {
                    Slime.ChangeState(new AttackState(Slime, attackTarget));
                }
            }

        }
    }
}
