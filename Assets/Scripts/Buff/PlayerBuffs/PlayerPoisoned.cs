using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace PlayerBuffs
{
    public class PlayerPoisoned : TimedBuff<PlayerBehaviour>
    {
        float _damage;
        float _interval;
        float _nextDamageTime;
        float _duration;
        public PlayerPoisoned(float duration, float damage, float interval) : base(duration)
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
                Debug.Log(ElapsedTime + " / " + Duration + " : took damage");
            }
        }

    }
}

