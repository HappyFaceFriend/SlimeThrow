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
            Color c = Slime.GetComponentInChildren<SpriteRenderer>().color;
            Slime.GetComponentInChildren<SpriteRenderer>().color = new Color(c.r, c.g, c.b, 0.5f);
            Slime.Flipper.enabled = false;
            SetAnimState();
        }

    }
}

