using System.Collections;
using System.Collections.Generic;
using UnityEditor;
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

