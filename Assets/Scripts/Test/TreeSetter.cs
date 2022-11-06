using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TreeSetter : MonoBehaviour
{
    [SerializeField] Vector2 _mapSize;
    [SerializeField] float _treeColumnXOffset;
    [SerializeField] float _treeColumnYOffset;
    [SerializeField] float _treeRowXOffset;
    [SerializeField] float _treeRowYOffset;
    [SerializeField] GameObject [] _trees;

    void GenerateTrees()
    {
        int colLen = Mathf.CeilToInt(_mapSize.y / _treeColumnYOffset);
        int rowLen = Mathf.CeilToInt(_mapSize.x / _treeRowXOffset);
        Transform col1 = GenerateColumn(colLen);
        Transform col2 = GenerateColumn(colLen);
        Transform row1 = GenerateRow(rowLen);
        Transform row2 = GenerateRow(rowLen);
        col1.position = new Vector3(-_mapSize.x / 2, -_mapSize.y / 2, 0);
        col2.position = new Vector3(_mapSize.x / 2, -_mapSize.y / 2, 0);
        row1.position = new Vector3(-_mapSize.x / 2, _mapSize.y / 2, 0);
        row2.position = new Vector3(-_mapSize.x / 2, -_mapSize.y / 2, 0);
    }
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Alpha2))
        {
            GenerateTrees();
        }
    }
    Transform GenerateColumn(int count)
    {
        Transform parent = new GameObject("col").transform;
        int toggle = 0;
        for (int i = 0; i < count; i++)
        {
            float x = _treeColumnXOffset / 2;
            if (toggle == 1)
                x *= -1;
            GameObject tree = Instantiate(_trees[toggle], new Vector3(x, i * _treeColumnYOffset, 0), Quaternion.identity);
            tree.transform.SetParent(parent);
            toggle = (toggle + 1) % 2;
        }
        return parent;
    }
    Transform GenerateRow(int count)
    {
        Transform parent = new GameObject("row").transform;
        int toggle = 0;
        for (int i = 0; i < count; i++)
        {
            float y = _treeRowYOffset / 2;
            if (toggle == 1)
                y *= -1;
            GameObject tree = Instantiate(_trees[toggle], new Vector3( i * _treeRowXOffset,y, 0), Quaternion.identity);
            tree.transform.SetParent(parent);
            toggle = (toggle + 1) % 2;
        }
        return parent;
    }
}
