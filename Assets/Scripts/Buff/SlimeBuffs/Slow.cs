using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace SlimeBuffs
{
    public class Slow : TimedBuff<SlimeBehaviour>
    {
        float _duration;
        Modifier _modifier;
        bool _start = false;
        public Slow(float duration) : base(duration)
        {
            _duration = duration;
            _modifier = new Modifier(0.5f, Modifier.ApplyType.Multiply);
        }
        public override void OnUpdate()
        {
            base.OnUpdate();
            if (!_start)
            {
                Owner.MoveSpeed.AddModifier(_modifier);
                _start = true;
            }
            else
            {
                if (ElapsedTime > _duration) 
                {
                    Owner.MoveSpeed.RemoveModifier(_modifier);
                    Debug.Log("원상복구");
                }
            }
        }

    }
}

