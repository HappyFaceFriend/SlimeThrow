using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SlimeState : StateBase
{
    protected SlimeBehaviour Slime { get; }
    public SlimeState(string name, SlimeBehaviour slime) : base(name, slime)
    {
        Slime = slime;
    }
}
