using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SlimeStates
{
    public class AttackState : SlimeState
    {
        Transform _attackTarget;
        SlimeAttackBase _attack;
        public AttackState(SlimeBehaviour slime, Transform target) : base("Attack", slime)
        {
            _attackTarget = target;
            _attack = Slime.GetComponent<SlimeAttackBase>();
        }

        public override void OnEnter()
        {
            base.OnEnter();
            _attack.StartAttack(_attackTarget);
            SetAnimState();
        }
        public override void OnUpdate()
        {
            base.OnUpdate();
            //if (Slime.Animator.GetCurrentAnimatorStateInfo(0).normalizedTime >= 1 && !Slime.Animator.IsInTransition(0))
            if(_attack.IsAttackDone)
            {
                Slime.ChangeState(new IdleState(Slime));
            }
        }
        public override void OnExit()
        {
            base.OnExit();
            _attack.StopAttack();
        }

    }
}

