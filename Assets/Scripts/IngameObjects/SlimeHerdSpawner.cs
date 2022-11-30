using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SlimeHerdSpawner : MonoBehaviour
{
    [SerializeField] Rect _spawnArea;
    [SerializeField] Rect  [] _dontSpawnAreas;

    SlimeHerd[] _allHerds;
    private void Awake()
    {
        _allHerds = Resources.LoadAll<SlimeHerd>(Defs.SlimeHerdPrefabsPath);
        StartCoroutine(StartSpawning());
    }
    private void OnDrawGizmos()
    {
        Gizmos.color = Color.green;
        Gizmos.DrawWireCube(_spawnArea.center, _spawnArea.size);
        Gizmos.color = Color.red;
        foreach(Rect rect in _dontSpawnAreas)
        {
            Gizmos.DrawWireCube(rect.center, rect.size);
        }
    }

    IEnumerator StartSpawning()
    {
        while(true)
        {
            SpawnHerdRandomPos(_allHerds[0]);
            yield return new WaitForSeconds(3f);
        }
    }




    void SpawnHerdRandomPos(SlimeHerd herd)
    {
        Vector2 targetPosition = Vector2.zero;
        bool isValid = false;
        while(!isValid)
        {
            targetPosition = Utils.Random.RandomVector(_spawnArea.xMin, _spawnArea.xMax, _spawnArea.yMin, _spawnArea.yMax);
            Rect herdRect = new Rect(targetPosition, herd.HerdSize);
            isValid = true;
            foreach (Rect rect in _dontSpawnAreas)
            {
                if (herdRect.Overlaps(rect))
                {
                    isValid = false;
                    break;
                }
            }
        }
        Instantiate(herd, targetPosition, Quaternion.identity);
    }
}
