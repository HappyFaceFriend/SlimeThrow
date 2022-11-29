using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SlimeStates
{
    public class HittedState : SlimeState
    {
        KnockbackController _knockback;
        public HittedState(SlimeBehaviour slime) : base("Hitted", slime) { }
        public override void OnEnter()
        {
            base.OnEnter();
            _knockback = Slime.GetComponent<KnockbackController>();
            Slime.Flipper.enabled = false;
            SetAnimState();
        }
        public override void OnUpdate()
        {
            base.OnUpdate();
            if (_knockback.IsKnockbackDone)
            {
                Slime.ChangeState(new MoveState(Slime));
            }
        }
        public override void OnExit()
        {
            base.OnExit();
            Slime.Flipper.enabled = true;
            Slime.Animator.ResetTrigger("Hitted");
        }
    }
}
