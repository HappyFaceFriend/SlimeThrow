using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SlimeStates
{
    public class DeadState : SlimeState
    {
        KnockbackController _knockback;
        public DeadState(SlimeBehaviour slime) : base("Hitted", slime) { }

        public override void OnEnter()
        {
            base.OnEnter();
            Slime.Flipper.enabled = false;
            SetAnimState();

            _knockback = Slime.GetComponent<KnockbackController>();
            Vector3 knockbackVec = _knockback.Velocity;
            _knockback.ApplyKnockbackDir(knockbackVec.normalized, 7, 10);
        }
        public override void OnUpdate()
        {
            base.OnUpdate();
            if(_knockback.IsKnockbackDone)
            {
                EffectManager.InstantiateHitEffect(Slime.transform.position);
                SoundManager.Instance.PlaySFX("SlimeExplode");
                GameObject.Destroy(Slime.gameObject);
            }
        }

    }
}

