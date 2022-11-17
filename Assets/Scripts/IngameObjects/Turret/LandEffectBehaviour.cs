using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LandEffectBehaviour: MonoBehaviour
{
    float _damage;
    List<LandEffectInfo> _landInfos;
    [SerializeField] float _duration;

    public void ApplyEffects(List<LandEffectInfo> landInfos, float additionalDamage)
    {
        _landInfos = landInfos;
        _damage = additionalDamage;
        foreach (var info in landInfos)
            _damage += info.Damage;
    }
    private void Start()
    {
        foreach (LandEffectInfo info in _landInfos)
        {
            info.GenerateEffect(transform.position);
        }
        Camera.main.GetComponent<CameraController>().Shake(CameraController.ShakePower.BulletLanded);
        Invoke("Kill", _duration);
    }
    void Kill()
    {
        Destroy(gameObject);
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        SlimeBehaviour slime = collision.GetComponent<SlimeBehaviour>();
        if (slime != null)
        {
            if(slime.IsAlive)
                slime.OnHittedByBullet(transform.position, _damage);
            foreach (LandEffectInfo info in _landInfos)
            {
                if(slime.IsAlive)
                {
                    info.OnHitMethod(slime, info.AdditionalInfos, transform.position);
                }
            }
        }
    }
}
