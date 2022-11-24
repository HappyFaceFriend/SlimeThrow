using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace PlayerBuffs
{
    public class PlayerStun : TimedBuff<PlayerBehaviour>
    {
        float _duration;
        Modifier _modifier;
        bool _start = false;
        float _probability;
        public PlayerStun(float duration, float probability) : base(duration)
        {
            _duration = duration;
            _modifier = new Modifier(0, Modifier.ApplyType.Multiply);
            _probability = probability;
        }
        public override void OnUpdate()
        {
            base.OnUpdate();
            if (!_start)
            {
                Debug.Log("스턴걸린다");
                Owner.MoveSpeed.AddModifier(_modifier);
                _start = true;
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

