using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TurretTargetingState : TurretState
{
    CameraController _camera;
    public TurretTargetingState(TurretBehaviour turret) : base("Targeting", turret) 
    { 
        _camera = Camera.main.GetComponent<CameraController>();
    }

    public override void OnEnter()
    {
        base.OnEnter();
        Turret.TargetMarker.position = Turret.transform.position;
        Turret.TargetMarker.gameObject.SetActive(true);
        Time.timeScale = 0.3f;
    }
    public override void OnUpdate()
    {
        base.OnUpdate();

        Vector3 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        mousePos.z = 0;

        Turret.TargetMarker.position = Vector3.MoveTowards(Turret.TargetMarker.position, mousePos, Turret.MarkerSpeed * Time.unscaledDeltaTime);

        if (Input.GetMouseButtonUp(1))
        {
            Turret.Shoot(Turret.TargetMarker.position);
            Turret.ChangeState(new TurretDefaultstate(Turret));
        }
    }
    public override void OnExit()
    {
        base.OnExit();
        Time.timeScale = 1f;
        Turret.TargetMarker.gameObject.SetActive(false);
        //_camera.Zoom(Vector3.zero, 1);
    }
}
