using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class KingProjectile : SlimeProjectile
{
    bool _split = false;
    [SerializeField] SlimeProjectile _childProjectile;
    SlimeBehaviour _source;
    Vector3 _shootedPosition;
    private void Start()
    {
        if (_slime.IsFever())
            _split = true;
    }
    public override void Init(Vector3 targetPosition, SlimeBehaviour shooter)
    {
        base.Init(targetPosition, shooter);
        _source = shooter;
        _shootedPosition = shooter.transform.position;
    }

   protected override void Die()
   {
        if (_split)
        { 
            Vector3 basedir = _moveDir;
            basedir = basedir.normalized;
            float baseAngle = GetAngle(transform.position, _shootedPosition);
            float offset = 15f;
            Vector3 dir1 = Utils.Vectors.AngleToVector(baseAngle + offset);
            Vector3 dir2 = Utils.Vectors.AngleToVector(baseAngle - offset);
            SlimeProjectile projectile1 = Instantiate(_childProjectile, transform.position, Quaternion.identity);
            SlimeProjectile projectile2 = Instantiate(_childProjectile, transform.position, Quaternion.identity);
            projectile1.Init(-dir1, 10);
            projectile2.Init(-dir2, 10);

        }
        Destroy(gameObject.gameObject);
    }
}