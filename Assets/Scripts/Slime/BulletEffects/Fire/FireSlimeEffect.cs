using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class FireSlimeEffect : SlimeBulletEffect
{
    public TinyFire _addtionalEffect;

    class FireEffectInfo : AdditionalInfo
    {
        public float burnProbability;
        public float burnLastTime;
        public int burnDamage;
        public FireEffectInfo()
        {
            this.burnProbability = 1f; // 100га╥н
            this.burnLastTime = 4f;
            this.burnDamage = 5;
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
        var fireInfo = info as FireEffectInfo;

    }
    public override void OnAddDuplicate(LandEffectInfo duplicateInfo)
    {
        duplicateInfo.Damage += Damage;
        FireEffectInfo fireInfo = new FireEffectInfo();
        fireInfo.burnDamage += 2;
        fireInfo.burnLastTime += 1;
        duplicateInfo.AdditionalInfos = fireInfo;
    }
    protected override AdditionalInfo GetAdditionalInfos()
    {
        var infos = new FireEffectInfo();
        return infos;
    }

    protected override string GetName()
    {
        return "FireSlime";
    }
}
