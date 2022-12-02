using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SlimeStates
{
    public class SpawnState : SlimeState
    {
        float duration = 0.67f;
        public SpawnState(SlimeBehaviour slime) : base("Spawn", slime)
        {
        }

        public override void OnEnter()
        {
            base.OnEnter();
            //슬라임 등장 사운드
            SetAnimState();
            Slime.StartCoroutine(Wait());
        }
        public override void OnUpdate()
        {
            base.OnUpdate();

        }

        IEnumerator Wait()
        {
            yield return new WaitForSeconds(duration);
            Slime.ChangeState(new IdleState(Slime));
        }
        public override void OnExit()
        {
            base.OnExit();
        }

    }
}

