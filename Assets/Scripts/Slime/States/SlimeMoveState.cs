using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SlimeMoveState : SlimeState
{
    SlimeMovement _movement;
    public SlimeMoveState(SlimeBehaviour slime) : base("Move", slime) { }

    public override void OnEnter()
    {
        Slime.Flipper.enabled = true;
        _movement = Slime.GetComponent<SlimeMovement>();
        _movement.OnStartMoving();
        SetAnimState();
    }

    public override void OnUpdate()
    {
        Slime.Flipper.targetPoint = _movement.TargetPos;
        _movement.OnUpdate();
        if (_movement.IsMovementDone)
            Slime.ChangeState(new SlimeIdleState(Slime));
    }
}
