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
        bool _active = false;
        Modifier _modifier;
        SlimeBehaviour _slime;
        public ElectricParalyse(float duration, float damage, SlimeBehaviour slime) : base(duration)
        {
            _duration = duration;
            _damage = damage;
            _interval = 1f;
            _modifier = new Modifier(0f, Modifier.ApplyType.Multiply);
            _slime = slime;
            _slime.ChangeState(new SlimeStates.FreezeState(slime));
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
            if (!_active)
            {
                Debug.Log("모디파이어 추가한다");
                Owner.MoveSpeed.AddModifier(_modifier);
                _active = true;
            }
            else if (ElapsedTime > _duration)
            {
                Debug.Log("모디파이어 제거한다");
                Owner.MoveSpeed.RemoveModifier(_modifier);
                _slime.ChangeState(new SlimeStates.MoveState(_slime));
            }
        }

    }
}

