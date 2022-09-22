using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireSlimeEffect : SlimeBulletEffect
{
    class FireEffectInfo : AdditionalInfo
    {
        public float r;
        public FireEffectInfo(float r)
        {
            this.r = r;
        }
    }
    protected override void GenerateEffect(GameObject effectPrefab, Vector3 landPosition)
    {
        GameObject effect = Instantiate(effectPrefab);
        effect.transform.position = landPosition;
    }
    protected override void OnHittedSlime(SlimeBehaviour slime, AdditionalInfo info, Vector3 landPosition)
    {
        var fireInfo = info as FireEffectInfo;
        slime.GetComponentInChildren<SpriteRenderer>().color = new Color(fireInfo.r, 0, 0);
    }
    public override void OnAddDuplicate(LandEffectInfo duplicateInfo)
    {
        duplicateInfo.Damage += Damage;
    }
    protected override AdditionalInfo GetAdditionalInfos()
    {
        var infos = new FireEffectInfo(255);
        return infos;
    }

    protected override string GetName()
    {
        return "FireSlime";
    }
}
