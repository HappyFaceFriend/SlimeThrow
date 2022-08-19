using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ThrownState : SlimeState
{
    Vector3 _moveDir;
    float _speed;
    public ThrownState(SlimeBehaviour slime, Vector3 mousePosition, float speed) : base("Thrown", slime) 
    {
        _moveDir = (mousePosition - Slime.transform.position).normalized;
        _speed = speed;
    }
    public override void OnUpdate()
    {
        Slime.transform.position += _moveDir * _speed * Time.deltaTime;
    }
}