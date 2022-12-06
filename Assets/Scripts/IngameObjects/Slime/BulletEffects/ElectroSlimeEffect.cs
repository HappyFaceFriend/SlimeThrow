using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ElectroSlimeEffect : SlimeBulletEffect
{
    class ElectroEffectInfo : AdditionalInfo
    {
        public float Probability { get; set; }
        public float Duration { get; set; }
        public int DamagePerTick { get; set; }
        public ElectroEffectInfo()
        {
            Probability = 1f;
            Duration = 2f;
            DamagePerTick = 5;
        }
    }
    protected override void GenerateEffect(GameObject effectPrefab, Vector3 landPosition)
    {
        GameObject effect;
        effect = Instantiate(effectPrefab, landPosition, Quaternion.identity);
        Destroy(effect.gameObject, 3.0f);
    }
    protected override void OnHittedSlime(SlimeBehaviour slime, AdditionalInfo info, Vector3 landPosition)
    {
        var ElectroInfo = info as ElectroEffectInfo;
        if(GlobalRefs.UpgradeManager.GetCount("백만볼트") >= 1 && slime.HPSystem.CurrentHp <= slime.HPSystem.MaxHp.Value * 0.5f)
            slime.ApplyBuff(new SlimeBuffs.ElectricParalyse(ElectroInfo.Duration + GlobalRefs.UpgradeManager.GetCount("지져버리기"), (ElectroInfo.DamagePerTick + GlobalRefs.UpgradeManager.GetCount("전류충전")) * 2, slime));
        else
            slime.ApplyBuff(new SlimeBuffs.ElectricParalyse(ElectroInfo.Duration + GlobalRefs.UpgradeManager.GetCount("지져버리기"), ElectroInfo.DamagePerTick + GlobalRefs.UpgradeManager.GetCount("전류충전"), slime));    
    }
    public override void OnAddDuplicate(LandEffectInfo duplicateInfo)
    {
        duplicateInfo.Damage += Damage;
    }
    protected override AdditionalInfo GetAdditionalInfos()
    {
        var infos = new ElectroEffectInfo();
        return infos;
    }

    protected override string GetName()
    {
        return "ElectroSlime";
    }
}
