using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GoldBehaviour : SlimeBehaviour
{
    [SerializeField] GameObject _item;
    protected override void Awake()
    {
        base.Awake();
    }
    private void OnDestroy()
    {
        base.OnDestroy();
        Instantiate(_item, transform.position, Quaternion.identity);
    }
}
