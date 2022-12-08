using System.Collections;
using System.Collections.Generic;
using UnityEditor.Tilemaps;
using UnityEngine;


namespace PlayerBuffs
{
    public class PlayerFreeze : TimedBuff<PlayerBehaviour>
    {
        IceCube _buff;
        float _duration;
        Modifier _modifier;
        bool _start = false;


        public PlayerFreeze(float duration) : base(duration)
        {
            _duration = duration;
            _modifier = new Modifier(0, Modifier.ApplyType.Multiply);

        }
        public override void OnStart()
        {
            base.OnStart();
            _buff = EffectManager.InstantiateIceCube();
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

