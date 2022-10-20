using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TimedBuff<T> : Buff<T> where T:StateMachineBase
{
    float _eTime;
    float _duration;

    public float ElapsedTime { get { return _eTime; } }
    public float Duration { get { return _duration; } }
    public TimedBuff(float duration)
    {
        _duration = duration;
    }
    public override bool IsOver()
    {
        return _eTime >= _duration;
    }
    public override void OnStart()
    {
        base.OnStart();
        _eTime = 0f;
    }
    public override void OnUpdate()
    {
        base.OnUpdate();
        _eTime += Time.deltaTime;
    }
}
