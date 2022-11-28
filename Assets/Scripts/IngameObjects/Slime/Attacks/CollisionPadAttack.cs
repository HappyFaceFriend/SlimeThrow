using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollisionPadAttack : SlimeAttackBase
{
    Transform _target;
    [SerializeField] CollisionPadtile _padtilePrefab;
    [SerializeField] BulletSmoke _smokePrefab;

    protected override void OnStartAttack(Transform targetTransform)
    {
        _target = targetTransform;
    }
   
    public void AnimEvent_ShootProjectile()
    {
        CollisionPadtile projectile = Instantiate(_padtilePrefab, transform.position, Quaternion.identity);
        projectile.Init(_target.position, Slime);
        BulletSmoke smoke = Instantiate(_smokePrefab, transform.position, Quaternion.identity);
        Destroy(smoke.gameObject, 1f);
        Destroy(projectile.gameObject, 1.8f);
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
