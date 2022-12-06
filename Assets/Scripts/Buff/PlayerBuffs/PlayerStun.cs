using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace PlayerBuffs
{
    public class PlayerStun : TimedBuff<PlayerBehaviour>
    {
        ElectricShock _buff;
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
            if(Random.Range(0f, 1f) <= _probability)
            {
                if (!_start)
                {
                    _buff = EffectManager.InstantiateShock();
                    _buff.transform.SetParent(Owner.transform);
                    _buff.transform.localPosition = Vector3.zero;
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
        public override void OnEnd()
        {
            base.OnEnd();
            Owner.gameObject.GetComponentInChildren<ElectricShock>().gameObject.SetActive(false);
        }
    }
}

