using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SlimeHerd : MonoBehaviour
{
    [SerializeField] float spawnInterval = 0.1f;
    SlimeBehaviour [] _slimes;

    private void Awake()
    {
        _slimes = GetComponentsInChildren<SlimeBehaviour>();
        foreach (var slime in _slimes)
            slime.gameObject.SetActive(false);
        StartCoroutine(SpawnCoroutine());
    }
    IEnumerator SpawnCoroutine()
    {
        foreach(SlimeBehaviour slime in _slimes)
        {
            StartCoroutine(SpawnSingleSlime(slime));
            yield return new WaitForSeconds(spawnInterval);
        }
    }
    IEnumerator SpawnSingleSlime(SlimeBehaviour slime)
    {
        EffectManager.InstantiateSpawnWarning(slime.transform.position, slime);
        yield return null;
    }
}
