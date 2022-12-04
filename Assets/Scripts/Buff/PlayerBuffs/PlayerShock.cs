using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace PlayerBuffs
{
    public class PlayerShock : TimedBuff<PlayerBehaviour>
    {
        GameObject _shockPrefab;
         float _duration;
        Modifier _modifier;
        bool _start = false;
        float _probability;

        public PlayerShock(float duration, float probability, GameObject shockPrefab) : base(duration)
        {
            _duration = duration;
            _modifier = new Modifier(0, Modifier.ApplyType.Multiply);
            _probability = probability;
            _shockPrefab = shockPrefab;
        }
        public override void OnUpdate()
        {
            base.OnUpdate();
            if(Random.Range(0f, 1f) <= _probability)
            {
                if (!_start)
                {
                    Owner.MoveSpeed.AddModifier(_modifier);
                    _start = true;
                    Owner.InstantiateBuff(_shockPrefab, Owner.transform.position, _duration);
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
}

