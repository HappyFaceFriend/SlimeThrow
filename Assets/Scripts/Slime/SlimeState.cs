using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SlimeState : StateBase
{
    string _name;
    protected SlimeBehaviour Slime { get; }
    public SlimeState(string name, SlimeBehaviour slime) : base(name, slime)
    {
        Slime = slime;
        _name = name;
    }
    public void SetAnimState()
    {
        Slime.Animator.SetTrigger(_name);
    }
}
