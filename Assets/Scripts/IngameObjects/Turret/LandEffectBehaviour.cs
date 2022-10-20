using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LandEffectBehaviour: MonoBehaviour
{
    float _damage;
    List<LandEffectInfo> _landInfos;

    public void ApplyEffects(List<LandEffectInfo> landInfos)
    {
        _landInfos = landInfos;
        _damage = 0;
        foreach (var info in landInfos)
            _damage += info.Damage;
    }
    private void Start()
    {
        foreach (LandEffectInfo info in _landInfos)
        {
            info.GenerateEffect(transform.position);
        }
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        SlimeBehaviour slime = collision.GetComponent<SlimeBehaviour>();
        if (slime != null)
        {
            foreach (LandEffectInfo info in _landInfos)
            {
                if(slime.IsAlive)
                {
                    slime.OnHittedByBullet(transform.position, _damage);
                    info.OnHitMethod(slime, info.AdditionalInfos, transform.position);
                }
            }
        }
    }
}
