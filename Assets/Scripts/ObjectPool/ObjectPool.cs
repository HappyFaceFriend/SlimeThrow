using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectPool : MonoBehaviour
{
    [SerializeField] PooledObject _prefab;
    [SerializeField] int _size;
    List<PooledObject> objects;

    private void Awake()
    {
        objects = new List<PooledObject>();
        for(int i = 0; i < _size; i++)
        {
            PooledObject item = InstantiateItem();
            objects.Add(item);
        }
    }
    PooledObject InstantiateItem()
    {
        PooledObject item = Instantiate(_prefab, transform);
        item.gameObject.SetActive(false);
        item.SetPool(this);
        return item;
    }

    public T Create<T>(bool setActive = false) where T : PooledObject
    {
        T item;
        if(objects.Count > 0)
        {
            item = objects[0] as T;
            objects.RemoveAt(0);
        }
        else
        {
            item = InstantiateItem() as T;
        }

        item.gameObject.SetActive(setActive);
        item.transform.SetParent(null);
        return item;
    }
    public void Return(PooledObject item)
    {
        if(item.gameObject.activeSelf)
            item.gameObject.SetActive(false);
        item.transform.SetParent(transform);
        objects.Add(item);
    }
}
