using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CloudGenerator : MonoBehaviour
{
    [SerializeField] Transform[] _spawnPoints;
    [SerializeField] GameObject cloudPrefab;
    [SerializeField] float _interval;

    private void Start()
    {
        foreach (Transform spawnPoint in _spawnPoints)
        {
            StartCoroutine(StartSpawn(spawnPoint));
        }
    }
    IEnumerator StartSpawn(Transform spawnPoint)
    {
        while(true)
        {
            yield return new WaitForSeconds(_interval * Random.Range(0.9f, 1f));
            Instantiate(cloudPrefab, spawnPoint.position, Quaternion.identity);
        }
        
    }
}
