using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class SlimeBulletEffect : MonoBehaviour
{
    [SerializeField] int _damage;
    [SerializeField] GameObject _landEffectPrefab;

    public int Damage { get { return _damage; } }
    public void OnAddToTurret(BulletBuilder bulletBuilder)
    {
        LandEffectInfo info = new LandEffectInfo(GetName(), _damage, _landEffectPrefab,
                                            GenerateEffect, OnHittedSlime, OnAddDuplicate,
                                            GetAdditionalInfos());
        bulletBuilder.AddLandEffect(info);
    }
    protected virtual AdditionalInfo GetAdditionalInfos()
    {
        return new AdditionalInfo();
    }
    protected abstract string GetName();
    public abstract void OnAddDuplicate(LandEffectInfo duplicateInfo);
    protected abstract void GenerateEffect(GameObject effectPrefab, Vector3 landPosition);
    protected abstract void OnHittedSlime(SlimeBehaviour slime, AdditionalInfo info, Vector3 landPosition);
}
