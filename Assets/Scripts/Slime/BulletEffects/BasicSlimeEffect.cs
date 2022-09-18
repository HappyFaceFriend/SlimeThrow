using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BasicSlimeEffect : SlimeBulletEffect
{
    public override void OnAddToTurret(BulletBuilder bulletBuilder)
    {
        bulletBuilder.AddLandEffect(Log);
    }

    public void Log()
    {
        Debug.Log(name);
    }
}
