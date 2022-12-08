using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StickyFootStepGenerator : BuffEffectBase
{
    [SerializeField] float interval;
    [SerializeField] Vector3 randomOffset;
    float eTime = 0;

    private void OnEnable()
    {
        eTime = 0;
    }
    private void Update()
    {
        eTime += Time.deltaTime;
        if(eTime >= interval)
        {
            eTime -= interval;
            EffectManager.InstantiateStickyFootstep(transform.position + Utils.Random.RandomVector(-randomOffset/2, randomOffset/2));
        }
    }
}
