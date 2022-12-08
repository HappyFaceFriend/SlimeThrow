using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace PlayerStates
{
    public class InTurretState : PlayerState
    {
        float _invincibleBonusTime = 0.4f;
        public InTurretState(PlayerBehaviour player) : base("Default", player) { }
        public override void OnEnter()
        {
            base.OnEnter();
            SetAllSprites(false);
            GlobalRefs.Turret.PlacePlayer(Player);
            Player.SetInvincible(true);
            SetAnimState();
        }
        void SetAllSprites(bool enabled)
        {
            foreach(SpriteRenderer sprite in Player.GetComponentsInChildren<SpriteRenderer>())
            {
                sprite.enabled = enabled;
            }
        }
        public override void OnExit()
        {
            SetAllSprites(true);
            Player.SetInvincible(true, _invincibleBonusTime);
        }
    }

}