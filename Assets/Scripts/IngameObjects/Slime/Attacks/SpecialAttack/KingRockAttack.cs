using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KingRockAttack : MultiDirectionAttack
{
    public new void AnimEvent_ShootProjectile()
    {
        Vector3 baseVector = _target.position - transform.position;
        float baseAngle = GetAngle(baseVector);
        for (int i = 0; i < _numOfdirections; i++)
        {
            float offset = 0 + (360 / _numOfdirections) * i;
            float currentAngle = baseAngle + offset;
            Vector2 tempdir = Utils.Vectors.AngleToVector(currentAngle);
            Vector3 targetPos = new Vector3(tempdir.x, tempdir.y, 0) + transform.position;
            if(Slime.IsFever())
            {
                KingRockProjectile projectile = Instantiate(_projectilePrefab, transform.position, Quaternion.identity) as KingRockProjectile;
                projectile.Init(targetPos, Slime);
                projectile.SetProb(1f);
            }
            else
            {
                SlimeProjectile projectile = Instantiate(_projectilePrefab, transform.position, Quaternion.identity);
                projectile.Init(targetPos, Slime);
            }
        }
    }
}
