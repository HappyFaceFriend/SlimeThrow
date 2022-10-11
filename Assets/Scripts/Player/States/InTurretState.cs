using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace PlayerStates
{
    public class InTurretState : PlayerState
    {
        SpriteRenderer[] _sprites;
        public InTurretState(PlayerBehaviour player) : base("Default", player) { }
        public override void OnEnter()
        {
            base.OnEnter();
            _sprites = Player.GetComponentsInChildren<SpriteRenderer>();
            SetAllSprites(false);
            Turret.PlacePlayer(Player);
            Player.IsInvincible = true;
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
            Player.IsInvincible = false;
        }
    }

}