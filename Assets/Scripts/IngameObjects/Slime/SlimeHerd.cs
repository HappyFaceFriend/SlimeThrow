using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SlimeHerd : MonoBehaviour
{
    public enum Difficulty { Easy, Normal, Hard, VeryHard }
    [SerializeField] float spawnInterval = 0.1f;
    [SerializeField] Difficulty difficulty;
    [SerializeField] Vector2 _herdSize;
    [SerializeField] SlimeBehaviour _mainSlime;
    SlimeBehaviour [] _slimes;

    public Vector2 HerdSize { get { return _herdSize; } }
    public SlimeBehaviour MainSlime { get { return _mainSlime; } }

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
        Destroy(gameObject);
    }
    IEnumerator SpawnSingleSlime(SlimeBehaviour slime)
    {
        EffectManager.InstantiateSpawnWarning(slime.transform.position, slime);
        yield return null;
    }
    private void OnDrawGizmos()
    {
        Gizmos.DrawWireCube(Utils.Vectors.Vec3ToVec2(transform.position), HerdSize);
    }
}
