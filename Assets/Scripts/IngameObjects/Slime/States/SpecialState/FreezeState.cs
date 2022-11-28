using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SlimeStates
{
    public class FreezeState : SlimeState
    {
        public FreezeState(SlimeBehaviour slime) : base("Hitted", slime) { }
        public override void OnEnter()
        {
            base.OnEnter();
            Slime.Flipper.enabled = false;
            SetAnimState();
        }
        public override void OnUpdate()
        {
            base.OnUpdate();
        }
        public override void OnExit()
        {
            base.OnExit();
            Slime.Flipper.enabled = true;
        }
    }
}
