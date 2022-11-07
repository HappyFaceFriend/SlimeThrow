using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEditor.Experimental.GraphView.GraphView;
using static UnityEngine.GraphicsBuffer;

public class AngelMovement : SlimeMovement
{
    float _xoffset;
    float _yoffset;
    Vector3 _moveDir;
    protected override void Awake()
    {
        base.Awake();
        _xoffset = Random.Range(-2f, 2f);
        _yoffset = Random.Range(-2f, 2f);
        _moveDir = Vector3.zero - transform.position;
        _moveDir.Normalize();
    }
    public override void OnUpdate()
    {
        transform.position += _moveDir * _slime.MoveSpeed.Value * Time.deltaTime;
    }
}