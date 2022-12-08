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
        StickyFootStepGenerator _buff;
        public Slow(float duration, float slowPercent) : base(duration)
        {
            _duration = duration;
            _slowPercent = slowPercent;
            _modifier = new Modifier(1 - _slowPercent, Modifier.ApplyType.Multiply);
        }
    
        public override void OnUpdate()
        {
            base.OnUpdate();
            if (!_start)
            {
                Owner.MoveSpeed.AddModifier(_modifier);
                StickyFootStepGenerator buff = EffectManager.InstantiateStickyFootstepGenerator();
                buff.transform.SetParent(Owner.transform);
                buff.transform.localPosition = Vector3.zero;
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
            Owner.gameObject.GetComponentInChildren<StickyFootStepGenerator>().gameObject.SetActive(false);
        }

    }
}

