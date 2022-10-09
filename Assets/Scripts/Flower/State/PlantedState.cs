using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace FlowerStates
{
    public class PlantedState :FlowerState
    {
        public PlantedState(FlowerBehaviour flower) : base("planted", flower) { }

        public override void OnEnter()
        {
            base.OnEnter();
            SetAnimState();
        }
    }
}


