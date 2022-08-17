using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SlimeBehaviour : StateMachineBase
{

    new protected void Start()
    {
        
        base.Start();
    }


    protected override StateBase GetInitialState()
    {
        return new SlimeMoveState(this);
    }
}
