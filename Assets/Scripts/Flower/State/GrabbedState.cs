using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace FlowerStates
{
    public class GrabbedState : FlowerState
    {
        public GrabbedState(FlowerBehaviour flower) : base("Grabbed", flower) { }
        public override void OnEnter()
        {
            base.OnEnter();
            SetAnimState();
        }

    }
}