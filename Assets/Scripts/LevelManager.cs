using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelManager : SingletonBehaviour<LevelManager>
{
    [SerializeField] Flower _flower;
    [SerializeField] List<FlowerPlantPoint> _dirts;
    public Transform Flower { get { return _flower.transform; } }

    private void Start()
    {
        int dirtIdx = Random.Range(0, _dirts.Count);
        _dirts[dirtIdx].PlantFlower(_flower);
    }
}
