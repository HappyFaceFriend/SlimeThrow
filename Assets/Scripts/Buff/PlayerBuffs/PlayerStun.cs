using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace PlayerBuffs
{
    public class PlayerStun : TimedBuff<PlayerBehaviour>
    {
        StunStar _buff;
        float _duration;
        Modifier _modifier;
        bool _start = false;

        public PlayerStun(float duration) : base(duration)
        {
            _duration = duration;
            _modifier = new Modifier(0, Modifier.ApplyType.Multiply);
        }
        public override void OnStart()
        {
            base.OnStart();
            _buff = EffectManager.InstantiateStar();
            _buff.transform.SetParent(Owner.transform);
            _buff.transform.localPosition = new Vector3(0, 0.7f, 0);
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
                }
            }
        }
        public override void OnEnd()
        {
            base.OnEnd();
            if (_buff != null)
                _buff.Kill();
        }
    }
}

