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
            Probability = 1f;
            Duration = 4f;
            DamagePerTick = 5;
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
        PoisonEffectInfo poisonInfo = info as PoisonEffectInfo;
        slime.ApplyBuff(new SlimeBuffs.Poisoned(poisonInfo.Duration, poisonInfo.DamagePerTick, 0.5f));
    }
    public override void OnAddDuplicate(LandEffectInfo duplicateInfo)
    {
        duplicateInfo.Damage += Damage;
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
