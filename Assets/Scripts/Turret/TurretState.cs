using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TurretState : StateBase
{
    string _name;
    protected TurretBehaviour Turret { get; }
    public TurretState(string name, TurretBehaviour turret) : base(name, turret)
    {
        _name = name;
        Turret = turret;
    }
}
