using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireSlimeEffect : SlimeBulletEffect
{
    protected override void GenerateEffect(GameObject effectPrefab, Vector3 landPosition)
    {
        GameObject effect = Instantiate(effectPrefab);
        effect.transform.position = landPosition;
    }
    protected override void OnHittedSlime(SlimeBehaviour slime, Vector3 landPosition)
    {
        slime.GetComponentInChildren<SpriteRenderer>().color = new Color(255, 0, 0);
    }
    public override void OnAddDuplicate(LandEffectInfo duplicateInfo)
    {
        duplicateInfo.Damage += Damage;
    }
}
