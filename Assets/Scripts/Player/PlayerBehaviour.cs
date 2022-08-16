using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerBehaviour : StateMachineBase
{
    [SerializeField] private float _moveSpeed;
    public float MoveSpeed { get { return _moveSpeed; } }

    protected override StateBase GetInitialState()
    {
        return new PlayerIdleState(this);
    }

}
