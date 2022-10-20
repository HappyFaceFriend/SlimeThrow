using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SlimeSpawnerTest : MonoBehaviour
{
    [SerializeField] bool _stopSpawning = false;
    [SerializeField] float _interval;
    [SerializeField] Transform [] _spawnPoints;
    [SerializeField] SlimeBehaviour[] _spawnPattern;

    private void Start()
    {
        StartCoroutine(SpawnCoroutine());
    }

    IEnumerator SpawnCoroutine()
    {
        int spawnIdx = 0;
        while(!_stopSpawning)
        {
            var slime = Instantiate(_spawnPattern[spawnIdx]);
            slime.transform.position = _spawnPoints[Random.Range(0, _spawnPoints.Length)].position;
            spawnIdx = (spawnIdx + 1) % _spawnPattern.Length;
            yield return new WaitForSeconds(_interval);
        }
    }
}
