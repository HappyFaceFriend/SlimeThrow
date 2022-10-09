﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RangedAttack : SlimeAttackBase
{
    Transform _target;
    [SerializeField] SlimeProjectile _projectilePrefab;


    protected override void OnStartAttack(Transform targetTransform)
    {
        _target = targetTransform;
    }
    public void AnimEvent_ShootProjectile()
    {
        SlimeProjectile projectile = Instantiate(_projectilePrefab, transform.position, Quaternion.identity);
        projectile.SetTargetPos(_target.position);
    }

    protected override IEnumerator AttackCoroutine()
    {
        float eTime = 0f;
        while (eTime < Duration)
        {
            eTime += Time.deltaTime;
            yield return null;
        }
        IsAttackDone = true;
    }
}