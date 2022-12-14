using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LandEffectInfo
{
    public delegate void EffectGenerate(GameObject effectPrefab, Vector3 landPosition);
    public delegate void SlimeHit(SlimeBehaviour slime, AdditionalInfo info, Vector3 landPosition);
    public delegate void DuplicateAction(LandEffectInfo duplicateInfo);



    public GameObject EffectPrefab { get; private set; }
    public EffectGenerate EffectGenMethod { get; private set; }
    public SlimeHit OnHitMethod { get; private set; }
    public DuplicateAction OnAddDuplicate { get; private set; }
    public string Name { get; private set; }
    public float Damage { get; set; }

    public AdditionalInfo AdditionalInfos { get; set; }

    public LandEffectInfo(string name, float damage, GameObject effectPrefab, 
        EffectGenerate effectGenMethod, SlimeHit onHitMethod, DuplicateAction onAddDuplicate,
        AdditionalInfo additionalInfos)
    {
        Name = name;
        Damage = damage;
        EffectPrefab = effectPrefab;
        EffectGenMethod = effectGenMethod;
        OnHitMethod = onHitMethod;
        OnAddDuplicate = onAddDuplicate;
        AdditionalInfos = additionalInfos;
    }
    public void GenerateEffect(Vector3 landPosition)
    {
        EffectGenMethod(EffectPrefab, landPosition);
    }
}
