using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlowerPlantPoint : MonoBehaviour
{
    public bool IsFlowerPlanted { get { return _plantedFlower != null; } }
    Flower _plantedFlower;
    public void PlantFlower(Flower flower)
    {
        flower.transform.position = transform.position;
        flower.transform.SetParent(transform, true);
        _plantedFlower = flower;
    }
    public Flower RemoveFlower(Transform parent)
    {
        if (_plantedFlower == null)
            return null;
        Flower flower = _plantedFlower;
        flower.transform.SetParent(parent, true);

        _plantedFlower = null;
        return flower;
    }
}
