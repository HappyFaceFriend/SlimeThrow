using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TurretDefaultstate : TurretState
{
    public TurretDefaultstate(TurretBehaviour turret) : base("Default", turret) { }

    public override void OnUpdate()
    {
        base.OnUpdate();
        if (Input.GetMouseButtonDown(0) && Utils.Inputs.IsMouseOverGameObject(Turret.transform))
        {
            Turret.ChangeState(new TurretTargetingState(Turret));
        }

    }
}
