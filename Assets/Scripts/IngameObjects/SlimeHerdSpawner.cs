using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SlimeHerdSpawner : MonoBehaviour
{
    [SerializeField] Transform [] _spawnAreas;
    [SerializeField] float [] _areaWeights = new float[4];
    [SerializeField] Rect  [] _dontSpawnAreas;
    [Header("Test")]
    [SerializeField] float  timeScale = 1;
    [SerializeField] bool lighting = false;

    SlimeHerd[] _allHerds;
    private void Awake()
    {
        _allHerds = Resources.LoadAll<SlimeHerd>(Defs.SlimeHerdPrefabsPath);
        StartCoroutine(StartSpawning());
    }
    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        foreach(Rect rect in _dontSpawnAreas)
        {
            Gizmos.DrawWireCube(rect.center, rect.size);
        }
    }
    
    IEnumerator StartSpawning()
    {
        int idx = 0;
        int currentAreaIdx = 0;
        while(true)
        {
            Time.timeScale = timeScale;
            if (lighting)
                _spawnAreas[currentAreaIdx].GetComponent<SpriteRenderer>().enabled = true;
            currentAreaIdx = SelectNextArea(currentAreaIdx);
            if (lighting)
                _spawnAreas[currentAreaIdx].GetComponent<SpriteRenderer>().enabled = false;
            idx = (idx + 1) % _allHerds.Length;

            if (!lighting)
            SpawnHerdRandomPos(_allHerds[0], currentAreaIdx);
            yield return new WaitForSeconds(3f);
        }
    }

    Rect GetAreaRect(Transform area)
    {
        return new Rect(area.position - area.localScale / 2, area.localScale);
    }
    int GetNextAreaIdx(int current, int delta)
    {
        return (current + delta + _spawnAreas.Length) % _spawnAreas.Length;
    }
    int SelectNextArea(int current)
    {
        float weightSum = 0;
        for (int i = 0; i < _areaWeights.Length; i++)
            weightSum += _areaWeights[i];
        float r = Random.Range(0f, weightSum);
        int delta = 0;
        for(int i=0; i<_areaWeights.Length; i++)
        {
            if (r < _areaWeights[i])
            {
                delta = i;
                break;
            }
            else
                r -= _areaWeights[i];
        }
        return GetNextAreaIdx( current, (Random.Range(0, 2) * 2 - 1) * delta);

    }
    void SpawnHerdRandomPos(SlimeHerd herd, int areaIdx)
    {
        Vector2 targetPosition = Vector2.zero;
        bool isValid = false;
        Rect areaRect = GetAreaRect(_spawnAreas[areaIdx]);
        int count = 0;
        while (!isValid)
        {
            targetPosition = Utils.Random.RandomVector(
                                areaRect.xMin + herd.HerdSize.x / 2, areaRect.xMax - herd.HerdSize.x / 2,
                                areaRect.yMin + herd.HerdSize.y / 2, areaRect.yMax - herd.HerdSize.y / 2);
            Rect herdRect = new Rect(targetPosition - herd.HerdSize/2, herd.HerdSize);
            count++;
            isValid = true;
            foreach (Rect rect in _dontSpawnAreas)
            {
                if (herdRect.Overlaps(rect))
                {
                    isValid = false;
                    break;
                }
            }
            if (count > 25)
            {
                targetPosition = areaRect.center;
                break;
            }
        }
        print(count);
        Instantiate(herd, targetPosition, Quaternion.identity);
    }
}
