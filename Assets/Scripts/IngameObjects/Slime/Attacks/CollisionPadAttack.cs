using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollisionPadAttack : SlimeAttackBase
{
    Transform _target;
    [SerializeField] CollisionPadtile _padtilePrefab;

    protected override void OnStartAttack(Transform targetTransform)
    {
        _target = targetTransform;
    }
   
    public void AnimEvent_ShootProjectile()
    {
        CollisionPadtile projectile = Instantiate(_padtilePrefab, transform.position, Quaternion.identity);
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
