using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TurretTargetingState : TurretState
{
    public TurretTargetingState(TurretBehaviour turret) : base("Targeting", turret) { }

    public override void OnUpdate()
    {
        base.OnUpdate();

        if(Input.GetMouseButtonUp(1))
        {
            Turret.Shoot(Camera.main.ScreenToWorldPoint(Input.mousePosition));
            Turret.ChangeState(new TurretDefaultstate(Turret));
        }
    }
}
