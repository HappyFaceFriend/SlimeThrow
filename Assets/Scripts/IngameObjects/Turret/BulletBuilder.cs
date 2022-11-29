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
    public string _slimeName;
    public bool upgrade1 = false;
    float upgradeValue;

    int _count = 0;
    PlayerBehaviour _player = null;
    int _playerIdx = -1;
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
        bullet.ApplyEffects(realLandEffects, _player);
    }
    public void PushSlime(SlimeBehaviour slime)
    {
        _slots[_count].SetImage(slime.SlotIcon);
        _slimeName = slime.name;
        _count++;
        slime.BulletEffect.OnAddToTurret(this);
        SoundManager.Instance.PlaySFX("EnterTurret");
    }
    public void PushPlayer(PlayerBehaviour player)
    {
        _slots[_count].SetImage(player.SlotIcon);
        _player = player;
        _playerIdx = _count;
        _count++;
        SoundManager.Instance.PlaySFX("EnterTurret");
    }
    public void RemovePlayer()
    {
        if(_player != null)
        {
            for(int i= _playerIdx; i<_count; i++)
            {
                _slots[i].SetImage(_slots[i + 1].Icon);
            }
            _player = null;
            _playerIdx = -1;
            _count--;
        }
    }
    public void Clear()
    {
        _count = 0;
        for(int i=0; i<_slots.Length; i++)
        {
            _slots[i].SetImage(null);
        }
        _landEffectInfos.Clear();
        _player = null;
    }

    public void Upgrade(float value)
    {
        upgrade1 = true;
        upgradeValue = value;
    }

    public BulletBehaviour CreateBullet()
    {
        BulletBehaviour bulletObject = Instantiate(_bulletPrefab).GetComponent<BulletBehaviour>();
        if (upgrade1)
        {
            bulletObject._moveSpeed *= upgradeValue;
        }
        ApplyEffectsToBullet(bulletObject);
        Clear();
        return bulletObject;
    }
}
