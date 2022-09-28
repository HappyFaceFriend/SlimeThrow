using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SlimeIdleState : SlimeState
{
    SlimeMovement _movement;
    public SlimeIdleState(SlimeBehaviour slime) : base("Idle", slime) { }

    public override void OnEnter()
    {
        base.OnEnter();
        Slime.Flipper.enabled = true;
        _movement = Slime.GetComponent<SlimeMovement>();
    }
    public override void OnUpdate()
    {
        base.OnUpdate();
        Slime.Flipper.targetPoint = _movement.TargetPos;
    }
}
