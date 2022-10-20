using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CountedBuff<T> : Buff<T> where T : StateMachineBase
{
    int _dir;
    public int CurrentCount { get; private set; }
    public int TargetCount { get; private set; }
    public CountedBuff(int initialCount, int targetCount)
    {
        CurrentCount = initialCount;
        TargetCount = targetCount;
        _dir = TargetCount > CurrentCount ? 1 : -1;
    }
    public override bool IsOver()
    {
        if (_dir == 1)
            return CurrentCount >= TargetCount;
        else
            return CurrentCount <= TargetCount;
    }
    public void ChangeCount(int delta)
    {
        CurrentCount += delta;
    }
}
