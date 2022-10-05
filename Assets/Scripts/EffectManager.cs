using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EffectManager : MonoBehaviour
{
    static EffectManager _instance;
    
    [Header("Pools")]
    [SerializeField] ObjectPool _circleHitPool;
    private void Awake()
    {
        _instance = this;
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
}