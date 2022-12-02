using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class FireSlimeEffect : SlimeBulletEffect
{
    public TinyFire _addtionalEffect;
    public LittleFire _buffEffect;
    class BurnStat : AdditionalInfo
    {
        public float Probability { get; set; }
        public float Duration { get; set; }
        public int DamagePerTick { get; set; }
        public BurnStat()
        {
            Probability = 1f;
            Duration = 4f;
            DamagePerTick = 5;
        }
    }

    protected override void GenerateEffect(GameObject effectPrefab, Vector3 landPosition)
    {
        GameObject effect;
        TinyFire[] addtional = new TinyFire[3];
        Vector3[] directions = {new Vector3(1,2,0).normalized, new Vector3(2, -1, 0).normalized , new Vector3(-1, -1, 0).normalized };
        for (int i = 0; i < addtional.Length; i++)
        {
            addtional[i] = Instantiate(_addtionalEffect);
            addtional[i].transform.position = landPosition;
            addtional[i].SetDirection(directions[i]);
        }
        effect = Instantiate(effectPrefab);
        effect.transform.position = landPosition;
        Destroy(effect, 3.0f);
        Destroy(addtional[0], 3.0f);
        Destroy(addtional[1], 3.0f);
        Destroy(addtional[2], 3.0f);
    }
    protected override void OnHittedSlime(SlimeBehaviour slime, AdditionalInfo info, Vector3 landPosition)
    {
        float damage;
        if (slime.FlameBullet)  
            damage = slime.HPSystem.MaxHp.Value * 0.1f;
        else
            damage = GlobalRefs.EffectStatManager._burn.DamagePerTick.Value;
        slime.ApplyBuff(new SlimeBuffs.Burn(GlobalRefs.EffectStatManager._burn.Duration.Value, GlobalRefs.EffectStatManager._burn.DamagePerTick.Value, 0.5f));
        LittleFire buffEffect = Instantiate(_buffEffect);
        buffEffect.transform.SetParent(slime.transform, false);
        buffEffect.GetComponent<LittleFire>().SetDuration(GlobalRefs.EffectStatManager._burn.Duration.Value);

    }
    public override void OnAddDuplicate(LandEffectInfo duplicateInfo)
    {
        duplicateInfo.Damage += Damage;
        BurnStat burn = new BurnStat();
        burn.DamagePerTick += 2;
        burn.Duration += 1;
        duplicateInfo.AdditionalInfos = burn;
    }
    protected override AdditionalInfo GetAdditionalInfos()
    {
        var infos = new BurnStat(); 
        return infos;
    }

    protected override string GetName()
    {
        return "FireSlime";
    }
}
