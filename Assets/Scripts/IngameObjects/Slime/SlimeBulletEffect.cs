using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class SlimeBulletEffect : MonoBehaviour
{
    float _damage;
    [SerializeField] GameObject _landEffectPrefab;

    public float Damage { get { return _damage; } }

    private void Start()
    {
        _damage = GetComponent<SlimeBehaviour>().DamageAsBullet.Value;
    }
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
