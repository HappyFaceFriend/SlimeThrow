using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace PlayerBuffs
{
    public class PlayerShock : TimedBuff<PlayerBehaviour>
    {
        ElectricShock _buff;
        float _duration;
        Modifier _modifier;
        bool _start = false;

        public PlayerShock(float duration) : base(duration)
        {
            _duration = duration;
            _modifier = new Modifier(0, Modifier.ApplyType.Multiply);
        }
        public override void OnStart()
        {
            base.OnStart();
            _buff = EffectManager.InstantiateShock();
            _buff.transform.SetParent(Owner.transform);
            _buff.transform.localPosition = Vector3.zero;
            Owner.MoveSpeed.AddModifier(_modifier);
        }
        public override void OnUpdate()
        {
            base.OnUpdate();

        }
        public override void OnEnd()
        {
            base.OnEnd();
            Owner.MoveSpeed.RemoveModifier(_modifier);
            if (_buff != null)
                _buff.Kill();
        }
    }
}

