using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace SlimeBuffs
{
    public class Freeze : TimedBuff<SlimeBehaviour>
    {
        float _duration;
        Modifier _modifier;
        bool _start = false;
        public Freeze(float duration) : base(duration)
        {
            _duration = duration;
            _modifier = new Modifier(0f, Modifier.ApplyType.Multiply);
        }
        public override void OnUpdate()
        {
            base.OnUpdate();
            if (!_start)
            {
                Owner.MoveSpeed.AddModifier(_modifier);
                Owner.AttackSpeed.AddModifier(_modifier);
                _start = true;
            }
            else
            {
                if (ElapsedTime > _duration) // 얼어있는 동안
                {
                    Owner.MoveSpeed.RemoveModifier(_modifier);
                    Owner.AttackSpeed.RemoveModifier(_modifier);
                    Debug.Log("깨어났다");
                }
            }
        }

    }
}

