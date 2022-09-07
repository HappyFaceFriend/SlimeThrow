using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TurretState : StateBase
{
    protected TurretBehaviour Turret { get; }
    public TurretState(string name, TurretBehaviour turret) : base(name, turret)
    {
        Turret = turret;
    }
}
