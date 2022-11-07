using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RockSlimeEffect : SlimeBulletEffect
{
    class RockEffectInfo : AdditionalInfo
    {
        public float Probability { get; set; }
        public float Duration { get; set; }
        public int DamagePerTick { get; set; }
        public RockEffectInfo()
        {
        }
    }
    protected override void GenerateEffect(GameObject effectPrefab, Vector3 landPosition)
    {
        GameObject effect;
        effect = Instantiate(effectPrefab);
        effect.transform.position = landPosition;
        Destroy(effect, 3.0f);
    }
    protected override void OnHittedSlime(SlimeBehaviour slime, AdditionalInfo info, Vector3 landPosition)
    {
       

    }
    public override void OnAddDuplicate(LandEffectInfo duplicateInfo)
    {
        
    }
    protected override AdditionalInfo GetAdditionalInfos()
    {
        var infos = new RockEffectInfo();
        return infos;
    }

    protected override string GetName()
    {
        return "RockSlime";
    }
}
