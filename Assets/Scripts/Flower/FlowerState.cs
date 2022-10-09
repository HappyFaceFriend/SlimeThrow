using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlowerState : StateBase
{
    string _name;
    protected FlowerBehaviour Flower { get; }
    public FlowerState(string name, FlowerBehaviour flower) : base(name, flower)
    {
        Flower = flower;
        _name = name;
    }
    public void SetAnimState()
    {
        Flower.Animator.SetTrigger(_name);
    }
}
