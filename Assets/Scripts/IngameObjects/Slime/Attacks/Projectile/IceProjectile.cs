using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IceProjectile : SlimeProjectile
{
    public float _buffProbability;
    public float _duration;
    public new void Init(Vector3 targetPosition, SlimeBehaviour shooter)
    {
        base.Init(targetPosition, shooter);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        var target = collision.collider.GetComponent<PlayerBehaviour>();  // 꽃 공격하는 건 어떠카지
        if (target != null && target.IsTargetable)
        {
            target.OnHittedBySlime(_slime, _damage);
            if (Random.Range(0f, 1f) <= _buffProbability)
            {
                target.ApplyBuff(new PlayerBuffs.PlayerFreeze(_duration));
            }
            Die();
        }
    }

}
