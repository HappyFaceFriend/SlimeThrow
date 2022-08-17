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
}

