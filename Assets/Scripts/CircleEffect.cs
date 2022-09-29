using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CircleEffect : PooledObject
{
    [SerializeField] SpriteRenderer _circleSR;
    [SerializeField] SpriteRenderer [] _lines;

    Animator _animator;

    float _scale = 1;
    bool _noLine = false;
    public float Scale { get { return _scale; } set { transform.localScale = new Vector3(value, value, 0);  _scale = value; } }
    public bool NoLine { get { return _noLine; }  set { _lines[0].enabled = !value; _lines[1].enabled = !value; } }

    void Awake()
    {
        _animator = GetComponent<Animator>();
    }

    void Update()
    {
        if (_animator.GetCurrentAnimatorStateInfo(0).normalizedTime >= 1 && !_animator.IsInTransition(0))
        {
            ReturnToPool();
        }
    }
}
