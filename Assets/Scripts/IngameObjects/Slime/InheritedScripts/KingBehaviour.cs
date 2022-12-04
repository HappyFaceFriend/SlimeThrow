using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KingBehaviour : SlimeBehaviour
{

    protected override void OnDie()
    {
        ChangeState(new SlimeStates.DeadState(this));
    }

}
