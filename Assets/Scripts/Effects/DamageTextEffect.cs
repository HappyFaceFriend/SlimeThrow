using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using TMPro;

public class DamageTextEffect : PooledObject
{
    [SerializeField] TextMeshProUGUI _text;
    
    public void SetText(int damage)
    {
        _text.text = damage.ToString();
    }

    public void OnAnimEnd()
    {
        ReturnToPool();
    }
}
