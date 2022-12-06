using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace PlayerBuffs
{
    public class PlayerBurn : TimedBuff<PlayerBehaviour>
    {
        float _damage;
        float _interval;
        float _nextDamageTime;
        float _duration;
        GameObject _buff;
        public PlayerBurn(float duration, float damage, float interval, GameObject burnPrefab) : base(duration)
        {
            _duration = duration;
            _damage = damage;
            _interval = 1f;
            _nextDamageTime = 0f;
            _buff = burnPrefab;
        }

        public override void OnStart()
        {
            base.OnStart();
             Owner.InstantiateBuff(_buff, Owner.transform.position, _duration);
        }
        public override void OnUpdate()
        {
            base.OnUpdate();
            if ((ElapsedTime >= _nextDamageTime) && (ElapsedTime  <= _duration))
            {
                _nextDamageTime += _interval;
                Owner.TakeDamage(_damage);
            }
        }

    }
}

