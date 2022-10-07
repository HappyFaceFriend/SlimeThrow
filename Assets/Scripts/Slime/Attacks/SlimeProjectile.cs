using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SlimeProjectile : MonoBehaviour
{
    [SerializeField] float _range;
    [SerializeField] float _moveSpeed;

    Vector3 _moveDir = Vector3.zero;
    float _movedDistance = 0f;
    public void SetTargetPos(Vector3 targetPosition)
    {
        _moveDir = (targetPosition - transform.position).normalized;
    }

    public void Update()
    {
        transform.position += _moveDir * _moveSpeed * Time.deltaTime;
        _movedDistance += _moveSpeed * Time.deltaTime;
        if (_movedDistance < _range)
        {
            Destroy(gameObject);
        }
    }
}
