using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelManager : MonoBehaviour
{
    public Vector2 MapSize { get { return _mapSize; } }
    [SerializeField] Vector2 _mapSize;
    [SerializeField] Flower _flower;
    [SerializeField] List<FlowerPlantPoint> _dirts;

    [SerializeField] SlimeSpawner _spawner;

    private void Start()
    {
        StartCoroutine(GameLoop());
    }
    
    void InitFlower()
    {
        int dirtIdx = Random.Range(0, _dirts.Count);
        _dirts[dirtIdx].PlantFlower(_flower);
    }
    IEnumerator GameLoop()
    {
        //Init
        InitFlower();
        _spawner.Init();
        //Loop
        while (_spawner)
        {
            _spawner.StartNextStage();
            yield return _spawner.WaitUntilStageClear();

            //업그레이드

        }


    }

    public void OnPlayerDead()
    {

    }
}
