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
        float _slowPercent;
        PooledObject _buff;
        public Slow(float duration, float slowPercent) : base(duration)
        {
            _duration = duration;
            _slowPercent = slowPercent;
            _modifier = new Modifier(1 - _slowPercent, Modifier.ApplyType.Multiply);
        }
        public override void OnStart()
        {
            base.OnStart();
            _buff = EffectManager.InstantiateStickyFootstep();
            _buff.transform.SetParent(Owner.transform);
            _buff.transform.localPosition = Vector3.zero;
            _buff.gameObject.SetActive(true);
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
        public override void OnEnd()
        {
            base.OnEnd();
            Owner.gameObject.GetComponentInChildren<PooledObject>().gameObject.SetActive(false);
        }

    }
}

