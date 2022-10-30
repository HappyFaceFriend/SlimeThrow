using System.Collections;
using System.Collections.Generic;
using System.Net.Sockets;
using UnityEngine;

public class MultiDirectionAttack : SlimeAttackBase
{
    public int _numOfdirections;
    Transform _target;
    [SerializeField] SlimeProjectile _projectilePrefab;
    [SerializeField] BulletSmoke _smokePrefab;
    
    protected override void OnStartAttack(Transform targetTransform)
    {
        _target = targetTransform;
    }
    public void AnimEvent_ShootProjectile()
    {
        for(int i = 0; i < _numOfdirections; i++)
        {
            Vector3 targetPos = RotationMatrix(0 + (360/_numOfdirections) * i);
            SlimeProjectile projectile = Instantiate(_projectilePrefab, transform.position, Quaternion.identity);
            projectile.Init(targetPos, Slime);
        }
        BulletSmoke smoke = Instantiate(_smokePrefab, transform.position, Quaternion.identity);
        smoke.SetColor(name);
        Destroy(smoke.gameObject, 1f);
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

    private Vector3 RotationMatrix(float angle  )
    {
        float radian = angle * (float)(Mathf.PI / 180);
        float x2 = Mathf.Cos(radian) * (_target.position.x - transform.position.x) - Mathf.Sin(radian) * (_target.position.y - transform.position.y);
        float y2 = Mathf.Sin(radian) * (_target.position.x - transform.position.x) + Mathf.Cos(radian) * (_target.position.y - transform.position.y);
        return new Vector3(x2, y2, 0);
    }
}
