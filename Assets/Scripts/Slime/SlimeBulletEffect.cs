using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SlimeBulletEffect : MonoBehaviour
{
    [SerializeField] string _name;
    [SerializeField] int _damage;
    [SerializeField] GameObject _landEffectPrefab;

    public int Damage { get { return _damage; } }
    public void OnAddToTurret(BulletBuilder bulletBuilder)
    {
        LandEffectInfo info = new LandEffectInfo(_name, _damage, _landEffectPrefab,
                                            GenerateEffect, OnHittedSlime, OnAddDuplicate);
        bulletBuilder.AddLandEffect(info);
    }
    public virtual void OnAddDuplicate(LandEffectInfo duplicateInfo)
    {}

    protected virtual void GenerateEffect(GameObject effectPrefab, Vector3 landPosition)
    {}
    protected virtual void OnHittedSlime(SlimeBehaviour slime, Vector3 landPosition)
    {}
}
