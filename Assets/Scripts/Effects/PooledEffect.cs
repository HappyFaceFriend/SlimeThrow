using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PooledEffect : PooledObject
{
    Animator _animator;


    void Awake()
    {
        _animator = GetComponent<Animator>();
    }

    void Update()
    {
        if (_animator.GetCurrentAnimatorStateInfo(0).normalizedTime >= 1 && !_animator.IsInTransition(0))
        {
            ReturnToPool();
        }
    }
}
