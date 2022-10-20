using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SlimeStates
{
    public class GrabbedState : SlimeState
    {
        public GrabbedState(SlimeBehaviour slime) : base("Grabbed", slime) { }
        public override void OnEnter()
        {
            base.OnEnter();
            Slime.Flipper.enabled = false;
            SetAnimState();
        }
    }
}
