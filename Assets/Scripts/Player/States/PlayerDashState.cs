using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerDashState : PlayerState
{
    private Vector3 _dashDir;
    private float _eTime;
    private float _lastCurveValue;
    public PlayerDashState(PlayerBehaviour player) : base("Idle", player) { }

    public override void OnEnter()
    {
        _dashDir = Player.Inputs.LastMoveInput;
        _eTime = 0f;
        _lastCurveValue = Player.DashCurve.Evaluate(0);
    }
    public override void OnUpdate()
    {
        _eTime += Time.deltaTime;
        float process = Mathf.Clamp01(_eTime / Player.DashDuration);
        float curveValue = Player.DashCurve.Evaluate(process);
        Player.transform.position += _dashDir * (curveValue - _lastCurveValue) * Player.DashDistance;

        _lastCurveValue = curveValue;

        if (process == 1)
        {
            if (Player.Inputs.IsMovePressed)
                Player.ChangeState(new PlayerMoveState(Player));
            else
                Player.ChangeState(new PlayerIdleState(Player));
        }

    }
}
