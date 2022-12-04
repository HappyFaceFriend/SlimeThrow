using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HitEffectParticle : PooledObject
{
    public void Init(Color color)
    {
        float k = 0.85f;
        ParticleSystem.MainModule p = GetComponent<ParticleSystem>().main;
        p.startColor = new Color(color.r  * k, color.g * k, color.b * k);
    }

    private void OnParticleSystemStopped()
    {
        ReturnToPool();
    }
}
