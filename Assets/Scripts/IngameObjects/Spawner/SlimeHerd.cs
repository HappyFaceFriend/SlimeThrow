using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum Difficulty { Easy, Normal, Hard, VeryHard, Special }
public class SlimeHerd : MonoBehaviour
{
    [SerializeField] float _spawnInterval = 0.1f;
    [SerializeField] Difficulty _difficulty;
    [SerializeField] Vector2 _herdSize;
    [SerializeField] SlimeBehaviour _mainSlime;


    List<SlimeBehaviour> _slimes;
    RandomSlime [] _randomSlimes;

    public Vector2 HerdSize { get { return _herdSize; } }
    public Difficulty Difficulty { get { return _difficulty; } }
    public SlimeBehaviour MainSlime { get { return _mainSlime; } }

    private void Awake()
    {
        _slimes = new List<SlimeBehaviour>();
        _slimes.AddRange(GetComponentsInChildren<SlimeBehaviour>());
        _randomSlimes = GetComponentsInChildren<RandomSlime>();

        foreach (var randomSlime in _randomSlimes)
        {
            SlimeBehaviour newSlime = randomSlime.InstantiateRandom();
            newSlime.gameObject.SetActive(false);
            newSlime.transform.SetParent(transform);
            _slimes.Add(newSlime);
            Destroy(randomSlime.gameObject);
        }

        foreach (var slime in _slimes)
        {
            slime.gameObject.SetActive(false);
        }

        StartCoroutine(SpawnCoroutine());
    }
    IEnumerator SpawnCoroutine()
    {
        foreach(SlimeBehaviour slime in _slimes)
        {
            StartCoroutine(SpawnSingleSlime(slime));
            yield return new WaitForSeconds(_spawnInterval);
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
