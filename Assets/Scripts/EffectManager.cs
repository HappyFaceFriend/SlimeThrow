using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EffectManager : MonoBehaviour
{
    static EffectManager _instance;
    
    [SerializeField] GameObject _hitEffectPrefab;

    private void Awake()
    {
        _instance = this;
    }
    public static void InstantiateHiEffect(Vector3 position)
    {
        Quaternion rotation = Quaternion.Euler(new Vector3(0, 0, Random.Range(0, 360)));
        Instantiate(_instance._hitEffectPrefab, position, rotation);
    }
}
