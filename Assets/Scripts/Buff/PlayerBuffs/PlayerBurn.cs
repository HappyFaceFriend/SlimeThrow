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
        LittleFire _buff;
        public PlayerBurn(float duration, float damage, float interval) : base(duration)
        {
            _duration = duration;
            _damage = damage;
            _interval = 1f;
            _nextDamageTime = 0f;
        }

        public override void OnStart()
        {
            base.OnStart();
            _buff = EffectManager.InstantiateFireBurning();
            _buff.transform.SetParent(Owner.transform);
            _buff.transform.localPosition = Vector3.zero;
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
        public override void OnEnd()
        {
            base.OnEnd();
            Owner.gameObject.GetComponentInChildren<LittleFire>().gameObject.SetActive(false);
        }
    }
}

