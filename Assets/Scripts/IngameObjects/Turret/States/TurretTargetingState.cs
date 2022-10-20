using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TurretTargetingState : TurretState
{
    public TurretTargetingState(TurretBehaviour turret) : base("Targeting", turret) { }

    public override void OnEnter()
    {
        base.OnEnter();
    }
    public override void OnUpdate()
    {
        base.OnUpdate();

        if(Input.GetMouseButtonUp(1))
        {
            Vector3 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            mousePos.z = 0;
            Turret.Animator.SetTrigger("Shoot");
            Turret.Shoot(mousePos);
            Turret.ChangeState(new TurretDefaultstate(Turret));
        }
    }
}
