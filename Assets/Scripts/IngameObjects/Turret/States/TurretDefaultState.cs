using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TurretDefaultstate : TurretState
{
    public TurretDefaultstate(TurretBehaviour turret) : base("Default", turret) { }

    public override void OnEnter()
    {
        base.OnEnter();
    }
    public override void OnUpdate()
    {
        base.OnUpdate();
        if (Input.GetMouseButtonDown(1) && Turret.IsReadyToShoot)
        {
            Turret.ChangeState(new TurretTargetingState(Turret));
        }

    }
}
