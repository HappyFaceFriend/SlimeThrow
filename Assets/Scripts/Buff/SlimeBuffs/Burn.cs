using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.RuleTile.TilingRuleOutput;


namespace SlimeBuffs
{
    public class Burn : TimedBuff<SlimeBehaviour>
    {
        float _damage;
        float _interval;
        float _nextDamageTime;
        float _duration;
        public override void OnStart()
        {
            EffectManager.InstantiateFireBurning(Owner.transform.position);
        }
        public Burn(float duration, float damage, float interval) : base(duration)
        {
            _duration = duration;
            _damage = damage;
            _interval = 1f;
            _nextDamageTime = 0f;
        }
        public override void OnUpdate()
        {
            base.OnUpdate();
            if ((ElapsedTime >= _nextDamageTime) && (ElapsedTime <= _duration))
            {
                _nextDamageTime += _interval;
                Owner.TakeDamage(_damage);
            }
        }

    }
}

