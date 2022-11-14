using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace SlimeBuffs
{
    public class Frostbite : TimedBuff<SlimeBehaviour>
    {
        float _damage;
        float _interval;
        float _nextDamageTime;
        float _probability;
        float _duration;
        public Frostbite(float duration, float damage, float interval, float prob) : base(duration)
        {
            _duration = duration;
            _damage = damage;
            _interval = 1f;
            _nextDamageTime = 0f;
            _probability = prob;
        }
        public override void OnUpdate()
        {
            base.OnUpdate();
            if (Random.Range(0f, 1f) <= _probability)
            {
                if ((ElapsedTime >= _nextDamageTime) && (ElapsedTime <= _duration))
                {
                    _nextDamageTime += _interval;
                    Owner.TakeDamage(_damage);
                    Debug.Log(ElapsedTime + " / " + Duration + " : took damage");
                }
            }
        }

    }
}

