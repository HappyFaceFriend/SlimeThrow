using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SlimeStates
{
    public class AttackState : SlimeState
    {
        Transform _attackTarget;
        SlimeAttackBase _attack;
        KnockbackController _knockback;
        public AttackState(SlimeBehaviour slime, Transform target) : base("Attack", slime)
        {
            _attackTarget = target;
            _attack = Slime.GetComponent<SlimeAttackBase>();
            _knockback = Slime.GetComponent<KnockbackController>();
        }

        public override void OnEnter()
        {
            base.OnEnter();
            _knockback.SuperArmor = true;
            Slime.Flipper.TargetPoint = _attackTarget.position;
            if (_attackTarget == GlobalRefs.Flower.transform)
            {
                _attack.StartFlowerAttack(_attackTarget);
                Slime.Animator.SetBool("IsFlowerAttack", true);
            }
            else
            {
                _attack.StartAttack(_attackTarget);
                Slime.Animator.SetBool("IsFlowerAttack", false);
            }
            SoundManager.Instance.PlaySFX("SlimeAttack");
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
            _knockback.SuperArmor = false;
        }

    }
}

