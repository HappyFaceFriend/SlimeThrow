using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace SlimeBuffs
{
    public class ElectricParalyse : TimedBuff<SlimeBehaviour>
    {
        float _damage;
        float _interval;
        float _nextDamageTime;
        float _duration;
        public ElectricParalyse(float duration, float damage, float interval) : base(duration)
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
            if (ElapsedTime >= _duration)
                Owner.MoveSpeed.AddModifier(new Modifier(-Owner.MoveSpeed.BaseValue, Modifier.ApplyType.Add));
            else
                Owner.MoveSpeed.RemoveModifier(new Modifier(-Owner.MoveSpeed.BaseValue, Modifier.ApplyType.Add));
        }

    }
}

