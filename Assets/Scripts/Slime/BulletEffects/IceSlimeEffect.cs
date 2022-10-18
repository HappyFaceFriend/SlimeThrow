using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class IceSlimeEffect : SlimeBulletEffect
{
    class IceEffectInfo : AdditionalInfo
    {
        public float _freezeProb;
        public float _freezeTime;
        public float _frostbiteProb;
        public int _frostbiteDam;
        public int _frostbiteTime;
        public IceEffectInfo()
        {
            this._freezeProb = 1f;
            this._freezeTime = 1f;
            this._frostbiteProb = 0.3f;
            this._frostbiteDam = 3;
            this._freezeTime = 5;
        }
    }
    protected override void GenerateEffect(GameObject effectPrefab, Vector3 landPosition)
    {
        GameObject effects = Instantiate(effectPrefab);
        effects.transform.position = landPosition;
        Destroy(effects, 2.5f);
    }
    protected override void OnHittedSlime(SlimeBehaviour slime, AdditionalInfo info, Vector3 landPosition)
    {
        var iceInfo = info as IceEffectInfo;

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
