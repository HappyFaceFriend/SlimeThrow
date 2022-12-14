using System.Collections;
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
        Vector3 dir = _target.position - transform.position;
        dir.y = 0;
        SlimeProjectile projectile = Instantiate(_projectilePrefab, transform.position, Quaternion.identity);
        projectile.Init(_target.position, Slime);
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
