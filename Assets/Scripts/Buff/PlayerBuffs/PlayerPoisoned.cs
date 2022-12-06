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
        BuffBubble _buff;
        public PlayerPoisoned(float duration, float damage, float interval, GameObject buff) : base(duration)
        {
            _duration = duration;
            _damage = damage;
            _interval = 1f;
            _nextDamageTime = 0f;
            _buff = EffectManager.InstantiateBubble();
        }
        public override void OnStart()
        {
            base.OnStart();
            _buff = EffectManager.InstantiateBubble();
            _buff.transform.SetParent(Owner.transform);
            _buff.transform.localPosition = Vector3.zero;
            _buff.SetDuration(_duration);
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
        public override void OnEnd()
        {
            base.OnEnd();
            Owner.gameObject.GetComponentInChildren<BuffBubble>().gameObject.SetActive(false);
        }
    }
}

