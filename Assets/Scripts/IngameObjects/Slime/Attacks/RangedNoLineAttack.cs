using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.GraphicsBuffer;

public class RangedNoLineAttack : SlimeAttackBase
{
    Transform _target;
    [SerializeField] SlimePadtile _padtilePrefab;
    [SerializeField] BulletSmoke _smokePrefab;
    private float time = 0;
    protected override void OnStartAttack(Transform targetTransform)
    {
        _target = targetTransform;
    }
    public void AnimEvent_ShootProjectile()
    {
        SlimePadtile padtile = Instantiate(_padtilePrefab, transform.position, Quaternion.identity);
        padtile.Init(_target.position, Slime);
        BulletSmoke smoke = Instantiate(_smokePrefab, transform.position, Quaternion.identity);
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

