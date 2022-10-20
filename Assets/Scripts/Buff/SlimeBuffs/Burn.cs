using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace SlimeBuffs
{
    public class Burn : TimedBuff<SlimeBehaviour>
    {
        float _damage;
        float _interval;
        float _nextDamageTime;
        public Burn(float duration, float damage, float interval) : base(duration)
        {
            _damage = damage;
            _interval = interval;
            _nextDamageTime = interval;
        }
        public override void OnUpdate()
        {
            base.OnUpdate();
            if (ElapsedTime >= _nextDamageTime)
            {
                _nextDamageTime += _interval;
                Owner.TakeDamage(_damage);
                Debug.Log(ElapsedTime + " / " + Duration + " : took damage");
            }
        }

    }
}

