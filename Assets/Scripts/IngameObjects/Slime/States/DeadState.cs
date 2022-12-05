using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;


namespace SlimeStates
{
    public class DeadState : SlimeState
    {
        KnockbackController _knockback;
        SlimeMovement _movement;
        public DeadState(SlimeBehaviour slime) : base("Hitted", slime) { }

        public override void OnEnter()
        {
            base.OnEnter();
            Slime.Flipper.enabled = false;
            SetAnimState();

            _movement = Slime.GetComponent<SlimeMovement>();
            _knockback = Slime.GetComponent<KnockbackController>();
            Vector3 knockbackVec = _knockback.Velocity;
            if (knockbackVec == Vector3.zero)
                knockbackVec = (_movement.TargetPos - Slime.transform.position).normalized;
            _knockback.ApplyKnockbackDir(knockbackVec.normalized, 7, 10);

            //var buff = Slime.transform.GetChild(1).gameObject;
            //if (buff != null)
              //  buff.SetActive(false);
        }
        public override void OnUpdate()
        {
            base.OnUpdate();
            if(_knockback.IsKnockbackDone)
            {
                GameObject.Destroy(Slime.gameObject);
            }
        }

    }
}

