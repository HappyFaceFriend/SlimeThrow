using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StickySlimeEffect : SlimeBulletEffect
{
    class StickyEffectInfo : AdditionalInfo
    {
        public float Probability { get; set; }
        public float Duration { get; set; }
        public int DamagePerTick { get; set; }
        public StickyEffectInfo()
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
        var infos = new StickyEffectInfo();
        return infos;
    }

    protected override string GetName()
    {
        return "StickySlime";
    }
}
