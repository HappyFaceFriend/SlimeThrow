using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerState : StateBase
{
    string _name;
    protected PlayerBehaviour Player { get; }
    protected Transform transform { get { return Player.transform; } }
    public PlayerState(string name, PlayerBehaviour player) : base(name, player)
    {
        Player = player;
        _name = name;
    }
    public void SetAnimState()
    {
        int id = 0;
        if (_name == "Default")
            id = 0;
        else if (_name == "Dash")
            id = 1;
        else if (_name == "Grab")
            id = 2;
        else if (_name == "Attack")
            id = 3;
        else if (_name == "Dead")
            id = 4;
        Player.Animator.SetInteger("State", id);
    }

}

