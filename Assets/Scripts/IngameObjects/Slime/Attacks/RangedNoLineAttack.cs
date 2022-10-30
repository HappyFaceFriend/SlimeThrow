using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.GraphicsBuffer;

public class RangedNoLineAttack : SlimeAttackBase
{
    Transform _target;
    [SerializeField] GameObject _warningPrefab;
    [SerializeField] SlimePadtile _padtilePrefab;
    [SerializeField] BulletSmoke _smokePrefab;
    private float time = 0;
    protected override void OnStartAttack(Transform targetTransform)
    {
        _target = targetTransform;
    }
    public void AnimEvent_ShootProjectile()
    {
        GameObject warning = Instantiate(_warningPrefab, _target.position, Quaternion.identity);
        SlimePadtile padtile;
        BulletSmoke smoke;
        Destroy(warning, 1.5f);      
        if(time < 1.5f)
        {
            time += Time.deltaTime;
        }
        else
        {
            Debug.Log("¸¸µç´Ù");
            padtile = Instantiate(_padtilePrefab, _target.transform.position, Quaternion.identity);
            padtile.Init(_target.position, Slime);
            smoke = Instantiate(_smokePrefab, transform.position, Quaternion.identity);
            Destroy(smoke.gameObject, 1f);
            Destroy(padtile.gameObject, 1f);
            time = 0;
        }
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

