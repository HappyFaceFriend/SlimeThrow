using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace PlayerBuffs
{
    public class PlayerSlow : TimedBuff<PlayerBehaviour>
    {
        float _duration;
        Modifier _modifier;
        bool _active = false;
        public bool _finish = false;
        float _slowPercent;
        public PlayerSlow(float duration, float slowPercent) : base(duration)
        {
            _duration = duration;
            _slowPercent = slowPercent;
            _modifier = new Modifier(1 - _slowPercent, Modifier.ApplyType.Multiply);

        }
        public override void OnUpdate()
        {
            base.OnUpdate();
            if (!_active)
            { 
                Owner.MoveSpeed.AddModifier(_modifier);
                _active = true;
            }
            else
            {
                if (ElapsedTime > _duration)
                {
                    Owner.MoveSpeed.RemoveModifier(_modifier);
                }
            }
        }

    }
}

