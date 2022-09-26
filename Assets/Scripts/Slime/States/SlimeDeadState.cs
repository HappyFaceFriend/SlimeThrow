using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SlimeDeadState : SlimeState
{
    public SlimeDeadState(SlimeBehaviour slime) : base("Dead", slime) { }

    public override void OnEnter()
    {
        base.OnEnter();
        Color c = Slime.GetComponentInChildren<SpriteRenderer>().color;
        Slime.GetComponentInChildren<SpriteRenderer>().color = new Color(c.r, c.g, c.b, 0.5f);
        Slime.Flipper.enabled = false;
    }

}

