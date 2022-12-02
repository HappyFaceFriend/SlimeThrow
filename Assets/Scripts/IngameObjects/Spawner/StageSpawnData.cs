using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Spawn Data")]
public class StageSpawnData : ScriptableObject
{
    public float EasyWeight;
    public float NormalWeight;
    public float HardWeight;
    public float VeryHardWeight;

    public List<SlimeHerd> Bosses;
    public List<SlimeBehaviour> MainSlimeCandidates;

    public float MainSlimeWeight;

    public float SpawnInterval;
    public float Duration;
}
