using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PooledObject : MonoBehaviour
{
    ObjectPool _pool;
    public void SetPool(ObjectPool pool)
    {
        _pool = pool;
    }
    public void ReturnToPool()
    {
        gameObject.SetActive(false);
        _pool.Return(this);
    }
}
