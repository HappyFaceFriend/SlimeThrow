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
        GameObject[] effects = new GameObject[3];

        effects[0] = Instantiate(effectPrefab, landPosition, Quaternion.identity);

        effects[1] = Instantiate(effectPrefab, landPosition + new Vector3(0.5f, 0.2f, 0), Quaternion.identity);
        //effects[2] = Instantiate(effectPrefab, landPosition + new Vector3(-0.2f, 0.4f, 0), Quaternion.identity);
        Destroy(effects[0], 4.0f);
        Destroy(effects[1], 4.0f);
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
