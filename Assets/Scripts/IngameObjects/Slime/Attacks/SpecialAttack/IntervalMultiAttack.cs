using System.Collections;
using System.Collections.Generic;
using System.Data;
using UnityEngine;
using static UnityEngine.GraphicsBuffer;

public class IntervalMultiAttack : MultiDirectionAttack
{
    public float _interval = 0.05f;
    float _waitingTime;
    int _i;

 
    public new void AnimEvent_ShootProjectile()
    {
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
            if (_waitingTime <= _interval) // 아직 못 쏘는 때임
                _waitingTime += Time.deltaTime;
            else
            {
                Shoothing();
                yield return new WaitForSeconds(_interval);
                _waitingTime = 0f;
                
            }
        }
        IsAttackDone = true;
        _i = 0;
    }

    private void Shoothing()
    {
        Vector3 baseVector = _target.position - transform.position;
        float baseAngle = GetAngle(baseVector);
        if (_i < _numOfdirections)
        {
            float offset = 0 + (360 / _numOfdirections) * _i;
            float currentAngle = baseAngle + offset;
            Vector2 tempdir = Utils.Vectors.AngleToVector(currentAngle);
            Vector3 targetPos = new Vector3(tempdir.x, tempdir.y, 0) + transform.position;
            SlimeProjectile projectile = Instantiate(_projectilePrefab, transform.position, Quaternion.identity);
            projectile.Init(targetPos, Slime);
            _i++;
        }
    }
}
