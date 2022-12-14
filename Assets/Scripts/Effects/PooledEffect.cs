using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PooledEffect : PooledObject
{
    Animator _animator;
    Vector3 _velocity = Vector3.zero;

    void Awake()
    {
        _animator = GetComponent<Animator>();
    }

    public void SetVelocity(Vector3 velocity)
    {
        _velocity = velocity;
    }

    void Update()
    {
        transform.position += _velocity * Time.deltaTime;

        if (_animator.GetCurrentAnimatorStateInfo(0).normalizedTime >= 1 && !_animator.IsInTransition(0))
        {
            ReturnToPool();
        }
    }
}
