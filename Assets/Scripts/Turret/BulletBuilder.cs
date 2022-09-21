using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletBuilder : MonoBehaviour
{
    [SerializeField] GameObject _bulletPrefab;
    [SerializeField] GameObject _defaultLandPrefab;

    List<LandEffectInfo> _landEffectInfos = new List<LandEffectInfo>();
    public int Count { get { return _count; } }
    [SerializeField] SlimeSlot [] _slots;

    int _count = 0;
    private void Start()
    {
        Clear();
    }
    public void AddLandEffect(LandEffectInfo info)
    {
        _landEffectInfos.Add(info);
    }
    public void ApplyEffectsToBullet(BulletBehaviour bullet)
    {
        //특정 조합인경우는 여기서 체크
        List<LandEffectInfo> realLandEffects = new List<LandEffectInfo>();
        for(int i=0; i<_landEffectInfos.Count; i++)
        {
            var dup = realLandEffects.Find(x => x.Name == _landEffectInfos[i].Name);
            if (dup == null)
                realLandEffects.Add(_landEffectInfos[i]);
            else
                dup.OnAddDuplicate(dup);
        }
        bullet.ApplyEffects(realLandEffects);
    }
    public void PushSlime(SlimeBehaviour slime)
    {
        _slots[_count].SetImage(slime.SlotIcon);
        _count++;
        slime.BulletEffect.OnAddToTurret(this);
    }
    public void Clear()
    {
        _count = 0;
        for(int i=0; i<_slots.Length; i++)
        {
            _slots[i].SetImage(null);
        }
        _landEffectInfos.Clear();
    }

    public BulletBehaviour CreateBullet()
    {
        BulletBehaviour bulletObject = Instantiate(_bulletPrefab).GetComponent<BulletBehaviour>();
        ApplyEffectsToBullet(bulletObject);
        Clear();
        return bulletObject;
    }
}
