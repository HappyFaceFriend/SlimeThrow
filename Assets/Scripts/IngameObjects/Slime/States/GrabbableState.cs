using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SlimeStates
{
    public class GrabbableState : SlimeState
    {
        Utils.Timer _timer;
        public GrabbableState(SlimeBehaviour slime) : base("Grabbable", slime) { }

        public override void OnEnter()
        {
            base.OnEnter();
            Slime.Flipper.enabled = false;
            _timer = new Utils.Timer(Slime.GrabbableDuration);
            SetAnimState();
        }
        public override void OnUpdate()
        {
            base.OnUpdate();
            _timer.Tick();
            if(_timer.IsOver)
            {
                Slime.ChangeState(new DeadState(Slime));
            }
        }

    }
}

