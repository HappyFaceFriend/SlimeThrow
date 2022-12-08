using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.RuleTile.TilingRuleOutput;

public class KingIceProjectile : IceProjectile
{
    bool _split = false;
    [SerializeField] SlimeProjectile _childProjectile;
    private void Start()
    {
        if (_slime.IsFever())
            _split = true;
    }
    public new void Init(Vector3 targetPosition, SlimeBehaviour shooter)
    {
        base.Init(targetPosition, shooter);
    }

   protected override void Die()
   {
        if (_split)
        { 
            Vector3 basedir = _moveDir;
            basedir = basedir.normalized;
            float baseAngle = GetAngle(transform.position, _slime.transform.position);
            float offset = 15f;
            Vector3 dir1 = Utils.Vectors.AngleToVector(baseAngle + offset);
            Vector3 dir2 = Utils.Vectors.AngleToVector(baseAngle - offset);
            SlimeProjectile projectile1 = Instantiate(_childProjectile, transform.position, Quaternion.identity);
            SlimeProjectile projectile2 = Instantiate(_childProjectile, transform.position, Quaternion.identity);
            projectile1.Init(-dir1, 10);
            projectile2.Init(-dir2, 10);

        }
        Destroy(gameObject);
    }
}