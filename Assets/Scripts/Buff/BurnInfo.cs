using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BurnInfo : AdditionalInfo
{
    float Probability { get; set; }
    public BuffableStat Duration { get; set; }
    public BuffableStat DamagePerTick { get; set; }

    public BurnInfo()
    {
        Probability = 1f;
        Duration = new BuffableStat(4f);
        DamagePerTick = new BuffableStat(5);
    }
}
