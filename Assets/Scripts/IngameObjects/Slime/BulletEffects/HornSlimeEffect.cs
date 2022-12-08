using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HornSlimeEffect : SlimeBulletEffect
{
    protected override void GenerateEffect(GameObject effectPrefab, Vector3 landPosition)
    {
        
    }
    protected override void OnHittedSlime(SlimeBehaviour slime, AdditionalInfo info, Vector3 landPosition)
    {
        Debug.Log(slime.name + " Hitted by base bullet");
    }
    public override void OnAddDuplicate(LandEffectInfo duplicateInfo)
    {
        duplicateInfo.Damage += Damage; 
    }

    protected override string GetName()
    {
        return "HornSlime";
    }
}
