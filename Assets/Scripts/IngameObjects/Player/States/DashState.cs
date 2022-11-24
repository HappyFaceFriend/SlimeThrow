using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace PlayerStates
{
    public class DashState : PlayerState
    {
        Vector3 _dashDir;
        float _eTime;
        float _lastCurveValue;
        PlayerMovementSettings _settings;
        public DashState(PlayerBehaviour player) : base("Dash", player) { }

        public override void OnEnter()
        {
            base.OnEnter();
            _settings = Player.MovementSettings;
            _dashDir = Player.Inputs.LastMoveInput;
            _eTime = 0f;
            _lastCurveValue = _settings.DashCurve.Evaluate(0);
            Player.IsInvincible = true;
            Player.GetComponent<CircleCollider2D>().enabled = false;
            SoundManager.Instance.PlaySFX("PlayerRoll");
            SetAnimState();
        }
        public override void OnExit()
        {
            base.OnExit();
            Player.IsInvincible = false;
            Player.GetComponent<CircleCollider2D>().enabled = true;
        }
        public override void OnUpdate()
        {
            base.OnUpdate();
            _eTime += Time.deltaTime;
            float process = Mathf.Clamp01(_eTime / _settings.DashDuration);
            float curveValue = _settings.DashCurve.Evaluate(process);
            Player.transform.position += _dashDir * (curveValue - _lastCurveValue) * _settings.DashDistance;

            _lastCurveValue = curveValue;

            if (process == 1)
            {
                Player.ChangeState(new DefaultState(Player));
            }

        }
    }

}