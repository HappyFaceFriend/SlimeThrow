using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GrabbedState : SlimeState
{
    public GrabbedState(SlimeBehaviour slime) : base("Grabbed", slime) { }
    public override void OnEnter()
    {
        base.OnEnter();
        Color c = Slime.GetComponentInChildren<SpriteRenderer>().color;
        Slime.GetComponentInChildren<SpriteRenderer>().color = new Color(c.r, c.g, c.b, 1f);
        Slime.Flipper.enabled = false;
        SetAnimState();
    }
}
