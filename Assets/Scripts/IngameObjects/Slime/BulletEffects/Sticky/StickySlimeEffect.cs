using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StickySlimeEffect : SlimeBulletEffect
{
    class StickyEffectInfo : AdditionalInfo
    {
        public float Probability { get; set; }
        public float Duration { get; set; }
        public StickyEffectInfo()
        {
            Probability = 1f;
            Duration = 2.5f;
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
        StickyEffectInfo poisonInfo = info as StickyEffectInfo;
        slime.ApplyBuff(new SlimeBuffs.Slow(poisonInfo.Duration));
    }
    public override void OnAddDuplicate(LandEffectInfo duplicateInfo)
    {
        duplicateInfo.Damage += Damage;
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
