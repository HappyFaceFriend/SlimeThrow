using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEditor.Experimental.GraphView.GraphView;
using static UnityEngine.GraphicsBuffer;

public class AngelMovement : SlimeMovement
{
    float _xdir;
    float _ydir;
    Vector3 _moveDir;
    bool _activeRandom = false;
    protected override void Awake()
    {
        base.Awake();
        _moveDir = Vector3.zero - transform.position;
        _moveDir.Normalize();
    }
    public override void OnUpdate()
    {
        if (!_activeRandom)
        {
            if (Utils.Vectors.IsInDistance(Vector3.zero, transform.position, _slime.SightRange.Value))
                _activeRandom = true;
        }
        else
        {
            if (!IsInXBoundary())
                _moveDir.x = -_moveDir.x;
            if (!IsInYBoundary())
                _moveDir.y = -_moveDir.y;
        }
        transform.position += _moveDir * _slime.MoveSpeed.Value * Time.deltaTime;
    }

    IEnumerator RandomMove()
    {
        while (true)
        {
            _xdir = Random.Range(-1f, 1f);
            _ydir = Random.Range(-1f, 1f);
            yield return new WaitForSeconds(5);
            _moveDir = new Vector3(_xdir, _ydir).normalized;

        }
    }
    private bool IsInXBoundary()
    {
        if (-9 <= transform.position.x && transform.position.x <= 9)
            return true;
        else
            return false;
    }
    private bool IsInYBoundary()
    {
        if (-6 <= transform.position.y && transform.position.y <= 6)
            return true;
        else
            return false;
    }
}