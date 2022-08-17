using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SlimeMoveState : SlimeState
{
    SlimeMovement _movement;
    public SlimeMoveState(SlimeBehaviour slime) : base("GoToWell", slime) { }

    public override void OnEnter()
    {
        _movement = Slime.GetComponent<SlimeMovement>();
        _movement.OnStartMoving();
    }

    public override void OnUpdate()
    {
        _movement.OnUpdate();
        if (_movement.IsMovementDone)
            Slime.ChangeState(new SlimeIdleState(Slime));
    }
}
