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
        SlimeBehaviour _slime;
        IceCube _buff;
      
        public Freeze(float duration, SlimeBehaviour slime) : base(duration)
        {
            _duration = duration;
            _modifier = new Modifier(0f, Modifier.ApplyType.Multiply);
            _slime = slime;
            _slime.ChangeState(new SlimeStates.FreezeState(slime));
        }
        public override void OnUpdate()
        {
            base.OnUpdate();
            if (!_start)
            {
                Owner.MoveSpeed.AddModifier(_modifier);
                Owner.AttackSpeed.AddModifier(_modifier);
                _start = true;
                _buff = EffectManager.InstantiateIceCube();
                _buff.transform.SetParent(Owner.transform);
                _buff.transform.localPosition = Vector3.zero;
            }
            else
            {
                if (ElapsedTime > _duration) // ¾ó¾îÀÖ´Â µ¿¾È
                {
                    Owner.MoveSpeed.RemoveModifier(_modifier);
                    Owner.AttackSpeed.RemoveModifier(_modifier);
                    _slime.ChangeState(new SlimeStates.MoveState(_slime));
                    Debug.Log("±ú¾î³µ´Ù");
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

