using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace PlayerStates
{
    public class EnterTurretState : PlayerState
    {
        float _moveSpeed = 5f;
        public EnterTurretState(PlayerBehaviour player) : base("EnterTurret", player) 
        {
        }
        public override void OnEnter()
        {
            base.OnEnter();
            SetAnimState();
            Player.StartCoroutine(MoveCoroutine());
        }
        public override void OnExit()
        {
            base.OnExit();
        }
        IEnumerator MoveCoroutine()
        {
            Vector3 turretPos = GlobalRefs.Turret.transform.position;
            float z = 0;
            float _maxZHeight = 1f;
            float totalDistance = (turretPos - transform.position).magnitude;
            float movedDistance = 0;
            Vector3 moveDir = (turretPos - transform.position) / totalDistance;

            Vector3 lastShadowPos = transform.position;

            while (true)
            {
                Vector3 shadowPos = lastShadowPos + moveDir * _moveSpeed * Time.deltaTime;
                movedDistance += _moveSpeed * Time.deltaTime;
                if (movedDistance <= totalDistance / 2f)
                    z = Mathf.Lerp(0, _maxZHeight,
                        Utils.Curves.EaseOut(movedDistance / (totalDistance / 2)));
                else
                    z = Mathf.Lerp(_maxZHeight, GlobalRefs.Turret.Height,
                        Utils.Curves.EaseIn((movedDistance - totalDistance / 2) / (totalDistance / 2)));

                transform.position = shadowPos + new Vector3(0, z, 0);
                if (Utils.Vectors.IsPositionCrossed(turretPos, shadowPos, lastShadowPos))
                    break;

                lastShadowPos = shadowPos;
                yield return null;
            }
            Player.ChangeState(new InTurretState(Player));
        }
    }

}