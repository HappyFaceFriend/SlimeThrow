using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletBuilder : MonoBehaviour
{

    List<BulletBehaviour.LandEffect> _landEffects = new List<BulletBehaviour.LandEffect>();
    List<BulletBehaviour.FlyEffect> _flyEffects = new List<BulletBehaviour.FlyEffect>();
    public int Count { get { return _count; } }
    [SerializeField] SlimeSlot [] _slots;

    int _count = 0;
    private void Start()
    {
        Clear();
    }
    public void PushSlime(SlimeBehaviour slime)
    {
        _slots[_count].SetImage(slime.SlotIcon);
        _count++;
        slime.BulletEffect.OnAddToTurret(this);
    }
    public void ApplyEffectsToBullet(BulletBehaviour bullet)
    {
        bullet.ApplyEffects(_landEffects, _flyEffects);
    }
    public void Clear()
    {
        _count = 0;
        for(int i=0; i<_slots.Length; i++)
        {
            _slots[i].SetImage(null);
        }
        _landEffects.Clear();
        _flyEffects.Clear();
    }
    public void AddLandEffect(BulletBehaviour.LandEffect effect)
    {
        _landEffects.Add(effect);
    }
    public void AddFlyEffect(BulletBehaviour.FlyEffect effect)
    {
        _flyEffects.Add(effect);
    }
}
