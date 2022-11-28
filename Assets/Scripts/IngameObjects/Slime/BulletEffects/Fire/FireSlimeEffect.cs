using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class FireSlimeEffect : SlimeBulletEffect
{
    public TinyFire _addtionalEffect;
    public LittleFire _buffEffect;
    class BurnEffectInfo : AdditionalInfo
    {
        public float Probability { get; set; }
        public float Duration { get; set; }
        public int DamagePerTick { get; set; }
        public BurnEffectInfo()
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
        var fireinfo = info as BurnEffectInfo;
        if (GlobalRefs.UpgradeManager.GetCount("Flame Bullet") != 0)
            fireinfo.DamagePerTick = (int)(slime.HPSystem.MaxHp.Value / 10f);
        else
            fireinfo.DamagePerTick += GlobalRefs.UpgradeManager.GetCount("Burning Slime");
        slime.ApplyBuff(new SlimeBuffs.Burn(fireinfo.Duration + 3 * GlobalRefs.UpgradeManager.GetCount("Embers"), fireinfo.DamagePerTick , 0.5f));
        LittleFire buffEffect = Instantiate(_buffEffect);
        buffEffect.transform.SetParent(slime.transform, false);
        buffEffect.GetComponent<LittleFire>().SetDuration(fireinfo.Duration);
    }
    public override void OnAddDuplicate(LandEffectInfo duplicateInfo)
    {
        duplicateInfo.Damage += Damage;
        BurnEffectInfo fireinfo = new BurnEffectInfo();
        fireinfo.DamagePerTick += 2;
        fireinfo.Duration += 1;
        duplicateInfo.AdditionalInfos = fireinfo;
    }
    protected override AdditionalInfo GetAdditionalInfos()
    {
        var infos = new BurnEffectInfo(); 
        return infos;
    }

    protected override string GetName()
    {
        return "FireSlime";
    }
}
