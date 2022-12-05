using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EffectManager : MonoBehaviour
{
    static EffectManager _instance;

    [SerializeField] Canvas _canvas; 
    [Header("Pools")]
    [SerializeField] ObjectPool _circleHitPool;
    [SerializeField] ObjectPool _dustEffectPool;
    [SerializeField] ObjectPool _damageTextPool;
    [SerializeField] ObjectPool _smokeEffectPool;
    [SerializeField] ObjectPool _recoveryTextPool;
    [SerializeField] ObjectPool _spawnWarningPool;
    [SerializeField] ObjectPool _slimeHitParticlePool;
    [SerializeField] ObjectPool _slimeDieParticlePool;
    private void Awake()
    {
        _instance = this;
    }
    public static void InstantiateSlimeHitParticle(Vector3 position, Color color)
    {
        HitEffectParticle effect = _instance._slimeHitParticlePool.Create<HitEffectParticle>();
        effect.Init(color);
        effect.transform.position = position;
        effect.gameObject.SetActive(true);
    }
    public static void InstantiateSlimeDieParticle(Vector3 position, Color color)
    {
        HitEffectParticle effect = _instance._slimeDieParticlePool.Create<HitEffectParticle>();
        effect.Init(color);
        effect.transform.position = position;
        effect.gameObject.SetActive(true);
    }
    public static CircleEffect InstantiateHitEffect(Vector3 position, float scale = 1f, bool noLine = false)
    {
        Quaternion rotation = Quaternion.Euler(new Vector3(0, 0, Random.Range(0, 360)));
        CircleEffect effect = _instance._circleHitPool.Create<CircleEffect>();
        effect.transform.position = position;
        effect.transform.rotation = rotation;
        effect.Scale = scale;
        effect.NoLine = noLine;
        effect.gameObject.SetActive(true);
        return effect;
    }
    public static void InstantiateDustEffect(Vector3 position)
    {
        PooledObject effect = _instance._dustEffectPool.Create<PooledObject>();
        effect.transform.position = position;
        effect.gameObject.SetActive(true);
    }

    public static void InstantiateSmokeEffect(Vector3 position, Vector3 velocity, float scale = 1f)
    {
        PooledEffect effect = _instance._smokeEffectPool.Create<PooledEffect>();
        effect.SetVelocity(velocity);
        effect.transform.position = position;
        effect.transform.localScale = (new Vector3(scale, scale, 1));
        effect.gameObject.SetActive(true);
    }
    public static void InstantiateDamageTextEffect(Vector3 position, float damage, DamageTextEffect.Type type)
    {
        DamageTextEffect effect = _instance._damageTextPool.Create<DamageTextEffect>();
        effect.SetText((int)damage, type);
        effect.transform.SetParent(_instance._canvas.transform);
        effect.transform.position = position + new Vector3(0, 0.5f, 0);
        effect.transform.localScale = new Vector3(0.85f, 1, 1);
        effect.gameObject.SetActive(true);
    }

    public static void InstantiateSpawnWarning(Vector3 position, SlimeBehaviour slime)
    {
        SpawnWarning warning = _instance._spawnWarningPool.Create<SpawnWarning>();
        warning.transform.position = position;
        warning.Slime = slime;
        slime.transform.SetParent(null);
        warning.gameObject.SetActive(true);
        
    }
}
