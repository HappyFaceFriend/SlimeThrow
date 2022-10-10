using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelManager : SingletonBehaviour<LevelManager>
{

    [SerializeField] Flower _flower;
    [SerializeField] List<FlowerPlantPoint> _dirts;

    private void Start()
    {
        int dirtIdx = Random.Range(0, _dirts.Count);
        _dirts[dirtIdx].PlantFlower(_flower);
    }
}
