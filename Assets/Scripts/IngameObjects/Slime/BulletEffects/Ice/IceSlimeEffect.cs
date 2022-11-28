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
            this._frostbiteDamPerTick = 3;
            this._freezeTime = 2f;
            this._frostbiteDuration = 5f;
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
        var iceInfo = info as IceEffectInfo;
        slime.ApplyBuff(new SlimeBuffs.Freeze(iceInfo._freezeTime, slime, _freezeEffect));
        GameObject icecube = Instantiate(_freezeEffect);
        icecube.transform.parent = slime.transform;
        icecube.transform.localPosition = Vector3.zero;
        Destroy(icecube, iceInfo._freezeTime);
        slime.ApplyBuff(new SlimeBuffs.Frostbite(iceInfo._frostbiteDuration, iceInfo._frostbiteDamPerTick, 1f,iceInfo._frostbiteProb));
        GameObject snowing = Instantiate(_snowingEffect);
        snowing.transform.parent = slime.transform;
        snowing.transform.localPosition = new Vector3(0f, 0.05f);
        Destroy(snowing, iceInfo._frostbiteDuration);

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
