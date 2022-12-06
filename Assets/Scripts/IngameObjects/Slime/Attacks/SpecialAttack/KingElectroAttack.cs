using System.Collections;
using System.Collections.Generic;
using System.Net.Sockets;
using Unity.VisualScripting;
using UnityEngine;
public class KingElectroAttack : SlimeAttackBase
{
    public int _numOfdirections;
    protected Transform _target;
    [SerializeField] protected ElectroPadtile _projectilePrefab;
    [SerializeField] protected BulletSmoke _smokePrefab;
    float _magnitude;

    protected override void OnStartAttack(Transform targetTransform)
    {
        _target = targetTransform;
        _magnitude = (_target.position - transform.position).magnitude;
    }
    public void AnimEvent_ShootProjectile()
    {
        if(!Slime.IsFever())
        {
            Vector3 baseVector = _target.position - transform.position;
            float baseAngle = GetAngle(baseVector);
            ElectroPadtile projectile;
            for (int i = 0; i < _numOfdirections; i++)
            {
                float offset = 0 + (360 / _numOfdirections) * i;
                float currentAngle = baseAngle + offset;
                Vector2 tempdir = Utils.Vectors.AngleToVector(currentAngle);
                Vector3 targetPos = new Vector3(tempdir.x, tempdir.y, 0) * _magnitude + transform.position;
                projectile = Instantiate(_projectilePrefab, transform.position, Quaternion.identity);
                projectile.Init(targetPos, Slime);
                Destroy(projectile.gameObject, 1.8f);
            }
            BulletSmoke smoke = Instantiate(_smokePrefab, transform.position, Quaternion.identity);
            smoke.SetColor(name);
            Destroy(smoke.gameObject, 1f);
        }
        else
        {
            Vector3 baseVector = _target.position - transform.position;
            float baseAngle = GetAngle(baseVector);
            _numOfdirections += 5;
            ElectroPadtile projectile;
            for (int i = 0; i < _numOfdirections; i++)
            {
                float offset = 0 + (360 / _numOfdirections) * i;
                float currentAngle = baseAngle + offset;
                Vector2 tempdir = Utils.Vectors.AngleToVector(currentAngle);
                Vector3 targetPos = new Vector3(tempdir.x, tempdir.y, 0) * _magnitude * 1.5f + transform.position;
                projectile = Instantiate(_projectilePrefab, transform.position, Quaternion.identity);
                projectile.Init(targetPos, Slime);
                Destroy(projectile.gameObject, 1.8f);
            }
            BulletSmoke smoke = Instantiate(_smokePrefab, transform.position, Quaternion.identity);
            smoke.SetColor(name);
            _numOfdirections -= 5;
            Destroy(smoke.gameObject, 1f);
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

    protected float GetAngle(Vector3 vector)
    {
        Vector3 v = vector;
        return Mathf.Atan2(v.y, v.x) * Mathf.Rad2Deg;
    }
}
