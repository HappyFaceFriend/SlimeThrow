using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace PlayerStates
{
    public class InTurretState : PlayerState
    {
        float _invincibleBonusTime = 0.7f;
        SpriteRenderer[] _sprites;
        public InTurretState(PlayerBehaviour player) : base("Default", player) { }
        public override void OnEnter()
        {
            base.OnEnter();
            _sprites = Player.GetComponentsInChildren<SpriteRenderer>();
            SetAllSprites(false);
            GlobalRefs.Turret.PlacePlayer(Player);
            Player.SetInvincible(true);
            SetAnimState();
        }
        void SetAllSprites(bool enabled)
        {
            foreach(SpriteRenderer sprite in _sprites)
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