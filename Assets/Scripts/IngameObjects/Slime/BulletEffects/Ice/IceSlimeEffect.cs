using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class IceSlimeEffect : SlimeBulletEffect
{
    public GameObject _freezeEffect;
    public GameObject _snowingEffect;
    class IceEffectInfo : AdditionalInfo
    {
        public float _freezeProb;
        public float _freezeTime;
        public float _frostbiteProb;
        public int _frostbiteDamPerTick;
        public float _frostbiteDuration;
        public IceEffectInfo()
        {
            this._freezeProb = 1f;
            this._freezeTime = 1f;
            this._frostbiteProb = 1f;
            this._frostbiteDamPerTick = 1;
            this._frostbiteDuration = 5f;
        }
    }
    protected override void GenerateEffect(GameObject effectPrefab, Vector3 landPosition)
    {
        GameObject effect;
        effect = Instantiate(effectPrefab);
        effect.transform.position = landPosition;
        Destroy(effect.gameObject, 3.0f);
    }
    protected override void OnHittedSlime(SlimeBehaviour slime, AdditionalInfo info, Vector3 landPosition)
    {
        var iceInfo = info as IceEffectInfo;
        if (GlobalRefs.UpgradeManager.GetCount("ºù°áÅº") >= 1)
            iceInfo._freezeTime = 1000f;
        else
            iceInfo._freezeTime += GlobalRefs.UpgradeManager.GetCount("¾óÀ½ ¶¯");

        slime.ApplyBuff(new SlimeBuffs.Freeze(iceInfo._freezeTime, slime));
        if(GlobalRefs.UpgradeManager.GetCount("ÇÑÆÄ") >= 1)
        {
            slime.ApplyBuff(new SlimeBuffs.Frostbite(iceInfo._frostbiteDuration + GlobalRefs.UpgradeManager.GetCount("°¨±â"), 5, 1f, iceInfo._frostbiteProb));
            Modifier modifier = new Modifier(1.5f, Modifier.ApplyType.Multiply);
            slime.MoveSpeed.AddModifier(modifier);
        }
        else
            slime.ApplyBuff(new SlimeBuffs.Frostbite(iceInfo._frostbiteDuration + GlobalRefs.UpgradeManager.GetCount("°¨±â"), iceInfo._frostbiteDamPerTick + GlobalRefs.UpgradeManager.GetCount("¼³ºù"), 1f,iceInfo._frostbiteProb));

    }
    public override void OnAddDuplicate(LandEffectInfo duplicateInfo)
    {
        duplicateInfo.Damage += Damage;
        IceEffectInfo iceInfo = new IceEffectInfo();
        iceInfo._freezeTime += 1;
        iceInfo._frostbiteProb = 0.5f;
        duplicateInfo.AdditionalInfos = iceInfo;
    }
    protected override AdditionalInfo GetAdditionalInfos()
    {
        var infos = new IceEffectInfo();
        return infos;
    }

    protected override string GetName()
    {
        return "IceSlime";
    }

  
}
