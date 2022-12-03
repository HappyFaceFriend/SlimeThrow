using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TurretDefaultstate : TurretState
{

    float rotSpeed = 60;
    public TurretDefaultstate(TurretBehaviour turret) : base("Default", turret) { }

    public override void OnEnter()
    {
        base.OnEnter();
    }
    public override void OnUpdate()
    {
        base.OnUpdate();
        if(!Turret.IsShooting && Turret.BodyRotation.z != 0)
        {
            float rot = Turret.BodyRotation.z;
            if (rot > 180)
                rot -= 360;
            float dir = rot > 0 ? 1 : -1;
            Vector3 newRot = new Vector3(0, 0, rot - dir * rotSpeed * Time.deltaTime);
            if ((dir == -1 && newRot.z >= 0) || (dir == 1 & newRot.z <= 0))
                newRot.z = 0;
            Turret.SetBodyRotation(newRot);

        }
        if (Input.GetMouseButtonDown(1) && Turret.IsReadyToShoot)
        {
            Turret.ChangeState(new TurretTargetingState(Turret));
        }

    }
}
