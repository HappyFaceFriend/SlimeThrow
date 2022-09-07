using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerState : StateBase
{
    protected PlayerBehaviour Player { get; }
    public PlayerState(string name, PlayerBehaviour player) : base(name, player)
    {
        Player = player;
    }

    protected void MoveByInput()
    {
        if (Player.Inputs.IsMovePressed)
        {
            Player.transform.position += Player.Inputs.MoveInput * Player.MoveSpeed * Time.deltaTime;
        }
    }
}

