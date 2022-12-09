using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class FireSlimeEffect : SlimeBulletEffect
{
    public TinyFire _addtionalEffect;

    class BurnEffectInfo : AdditionalInfo
    {
        public float Probability { get; set; }
        public float Duration { get; set; }
        public int DamagePerTick { get; set; }
        public BurnEffectInfo()
        {
            Probability = 1f;
            Duration = 4f;
            DamagePerTick = 2;
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
        Destroy(effect.gameObject, 3.0f);
        Destroy(addtional[0].gameObject, 3.0f);
        Destroy(addtional[1].gameObject, 3.0f);
        Destroy(addtional[2].gameObject, 3.0f);
    }
    protected override void OnHittedSlime(SlimeBehaviour slime, AdditionalInfo info, Vector3 landPosition)
    {
        float damage;
        if (slime.FlameBullet)  
            damage = slime.HPSystem.MaxHp.Value * 0.1f;
        var fireinfo = info as BurnEffectInfo;
        if (slime.GetComponent<KingBehaviour>() == null && GlobalRefs.UpgradeManager.GetCount("È­¿°Åº") != 0)
            fireinfo.DamagePerTick = (int)(slime.HPSystem.MaxHp.Value / 10f);
        else
            fireinfo.DamagePerTick += GlobalRefs.UpgradeManager.GetCount("Å¸µé¾î°¡´Â ½½¶óÀÓ");
        slime.ApplyBuff(new SlimeBuffs.Burn(fireinfo.Duration + 3 * GlobalRefs.UpgradeManager.GetCount("ºÒ¾¾"), fireinfo.DamagePerTick , 0.5f));

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
