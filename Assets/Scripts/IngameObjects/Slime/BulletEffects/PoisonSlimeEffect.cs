using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PoisonSlimeEffect : SlimeBulletEffect
{
    class PoisonEffectInfo : AdditionalInfo
    {
        public float Probability { get; set; }
        public float Duration { get; set; }
        public int DamagePerTick { get; set; }
        public PoisonEffectInfo()
        {
           
        }
    }
    protected override void GenerateEffect(GameObject effectPrefab, Vector3 landPosition)
    {
       
    }
    protected override void OnHittedSlime(SlimeBehaviour slime, AdditionalInfo info, Vector3 landPosition)
    {
       
    }
    public override void OnAddDuplicate(LandEffectInfo duplicateInfo)
    {
       
    }
    protected override AdditionalInfo GetAdditionalInfos()
    {
        var infos = new PoisonEffectInfo();
        return infos;
    }

    protected override string GetName()
    {
        return "PoisonSlime";
    }
}
