using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SlimeStates
{
    public class DeadState : SlimeState
    {
        public DeadState(SlimeBehaviour slime) : base("Dead", slime) { }

        public override void OnEnter()
        {
            base.OnEnter();
            Slime.Flipper.enabled = false;
            SetAnimState();
        }
        public override void OnUpdate()
        {
            base.OnUpdate();
            if (Slime.Animator.GetCurrentAnimatorStateInfo(0).normalizedTime >= 1 && !Slime.Animator.IsInTransition(0))
            {
                GameObject.Destroy(Slime.gameObject);
            }
        }

    }
}

