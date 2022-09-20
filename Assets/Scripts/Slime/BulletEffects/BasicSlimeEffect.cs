using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BasicSlimeEffect : SlimeBulletEffect
{

    [SerializeField] BulletLandEffect _landEffect;
    [SerializeField] int _landDamage;
    public override void OnAddToTurret(BulletBuilder bulletBuilder)
    {
        bulletBuilder.AddLandEffect(LandEffect);
    }

    public void LandEffect(Vector3 landPosition)
    {
        BulletLandEffect effect = Instantiate(_landEffect);
        effect.Init(_landDamage);
        effect.transform.position = landPosition;
    }
}
