using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnWarning : PooledObject
{
    public SlimeBehaviour Slime { get; set; }
    public void AnimEvent_Spawn()
    {
        Slime.gameObject.SetActive(true);
        Slime.transform.SetParent(null);
    }
    public void AnimEvent_Destroy()
    {
        ReturnToPool();
    }
}
