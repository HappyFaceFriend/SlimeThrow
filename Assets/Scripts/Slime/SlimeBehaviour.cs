using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SlimeBehaviour : StateMachineBase
{
    [SerializeField] int maxHp;
    public bool IsGrabbable { get { return CurrentState is SlimeDeadState; } }
    

    new protected void Start()
    {
        base.Start();
    }

    public void SetGrabbed(GrabController grabController)
    {
        transform.SetParent(grabController.transform);
        transform.localPosition = Vector3.zero;
        ChangeState(new GrabbedState(this));

    }
    public void SetThrown(Vector3 mousePosition, float throwSpeed)
    {
        transform.SetParent(null);

        ChangeState(new ThrownState(this, mousePosition, throwSpeed));
    }
    public void OnHitted(PlayerBehaviour player)
    {
        Debug.Log(name + " HIT!");
        ChangeState(new SlimeDeadState(this));
    }

    protected override StateBase GetInitialState()
    {
        return new SlimeMoveState(this);
    }
}
