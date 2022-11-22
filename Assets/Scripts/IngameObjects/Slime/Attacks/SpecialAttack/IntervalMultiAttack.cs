using System.Collections;
using System.Collections.Generic;
using System.Data;
using UnityEngine;
using static UnityEngine.GraphicsBuffer;

public class IntervalMultiAttack : MultiDirectionAttack
{
    public float _interval = 0.05f;
    float _waitingTime;
    public new void AnimEvent_ShootProjectile()
    {
        BulletSmoke smoke = Instantiate(_smokePrefab, transform.position, Quaternion.identity);
        StartCoroutine("Shooting");
        smoke.SetColor(name);
        Destroy(smoke.gameObject, 1f);
    }
    protected override IEnumerator AttackCoroutine()
    {
        float eTime = 0f;
        while (eTime < Duration)
        {
            eTime += Time.deltaTime;
            if (_waitingTime <= _interval)
                _waitingTime += Time.deltaTime;
            else
                _waitingTime = 0;
            yield return null;
        }
        IsAttackDone = true;
    }

    IEnumerator Shoothing()
    {
        Vector3 baseVector = _target.position - transform.position;
        float baseAngle = GetAngle(baseVector);
        int i = 0;
        while (i <= _numOfdirections)
        {
            float offset = 0 + (360 / _numOfdirections) * i;
            float currentAngle = baseAngle + offset;
            Vector2 tempdir = Utils.Vectors.AngleToVector(currentAngle);
            Vector3 targetPos = new Vector3(tempdir.x, tempdir.y, 0) + transform.position;
            SlimeProjectile projectile = Instantiate(_projectilePrefab, transform.position, Quaternion.identity);
            projectile.Init(targetPos, Slime);
            yield return new WaitForSeconds(_interval);
        }
    }
}
