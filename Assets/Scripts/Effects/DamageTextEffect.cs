using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using TMPro;

public class DamageTextEffect : PooledObject
{
    public enum Type { PlayerHitted, SlimeHitted, FlowerHitted}

    Type _type;
    [SerializeField] TextMeshProUGUI _text;

    [SerializeField] Animator _animator;
    public void SetText(int damage, Type type)
    {
        _text.text = damage.ToString();
        _type = type;
    }
    private void OnEnable()
    {

        _animator.SetTrigger(_type.ToString());
    }
    public void OnAnimEnd()
    {
        ReturnToPool();
    }
}
