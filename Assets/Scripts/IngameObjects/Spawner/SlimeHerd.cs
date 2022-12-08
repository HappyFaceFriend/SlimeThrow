using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum Difficulty { Easy, Normal, Hard, VeryHard, Special, Boss }
public class SlimeHerd : MonoBehaviour
{
    [SerializeField] float _spawnInterval = 0.1f;
    [SerializeField] Difficulty _difficulty;
    [SerializeField] Vector2 _herdSize;
    [SerializeField] SlimeBehaviour _mainSlime;

    List<SpawnWarning> _warnings;

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
        _warnings = new List<SpawnWarning>();

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
            Vector3 spawnPosition = slime.transform.position;
            if (transform.position.x > 0)
                spawnPosition.x *= -1;
            if (transform.position.y < 0)
                spawnPosition.y *= -1;
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
        while(_warnings.Count > 0)
        {
            _warnings.RemoveAll(x => !x.gameObject.activeInHierarchy);
            yield return null;
        }
        Destroy(gameObject);
    }
    IEnumerator SpawnSingleSlime(SlimeBehaviour slime)
    {
        if (GlobalRefs.LevelManger.IsGameOver)
            yield  break;
        _warnings.Add( EffectManager.InstantiateSpawnWarning(slime.transform.position, slime) );
        if(GlobalRefs.LevelManger.Spawner._isBurningOn && !GlobalRefs.LevelManger.Spawner._stopBurn)
            slime.ApplyBuff(new SlimeBuffs.Burn(8f, 1, 0.8f));
        if(GlobalRefs.LevelManger.Spawner._isSnowyOn && !GlobalRefs.LevelManger.Spawner._stopSnowy)
            slime.ApplyBuff(new SlimeBuffs.Frostbite(8f, 1, 0.8f, 1f));
        yield return null;
    }
    private void OnDrawGizmos()
    {
        Gizmos.DrawWireCube(Utils.Vectors.Vec3ToVec2(transform.position), HerdSize);
    }
}
