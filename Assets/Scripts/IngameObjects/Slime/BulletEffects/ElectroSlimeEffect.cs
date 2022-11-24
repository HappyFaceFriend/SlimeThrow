using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ElectroSlimeEffect : SlimeBulletEffect
{
    public GameObject _buffEffect;
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
        Destroy(effect, 3.0f);
    }
    protected override void OnHittedSlime(SlimeBehaviour slime, AdditionalInfo info, Vector3 landPosition)
    {
        var ElectroInfo = info as ElectroEffectInfo;
        slime.ApplyBuff(new SlimeBuffs.ElectricParalyse(ElectroInfo.Duration, ElectroInfo.DamagePerTick, slime));
        GameObject buffEffect = Instantiate(_buffEffect);
        buffEffect.transform.SetParent(slime.transform, false);
        Destroy(slime.transform.GetChild(1).gameObject, ElectroInfo.Duration);
        
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
