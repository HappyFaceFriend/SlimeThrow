using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SlimeBehaviour : StateMachineBase
{
    [SerializeField] int _maxHp;
    [SerializeField] Sprite _slotIcon;
    public bool IsGrabbable { get { return CurrentState is SlimeDeadState; } }
    public Sprite SlotIcon { get { return _slotIcon; } }

    new protected void Start()
    {
        base.Start();
    }

    public void SetGrabbed(GrabController grabController)
    {
        ChangeState(new GrabbedState(this));
    }
    public void OnPlacedAtTurret()
    {

    }
    public void OnReleasedAtGround()
    {
        ChangeState(new SlimeDeadState(this));
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
