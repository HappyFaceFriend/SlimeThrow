using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LittleFire : BuffEffectBase
{
    Animator _animator;
    //SpriteRenderer _spriteRenderer;
    float _timer = 0;
    bool _finish = false;
    bool _looping = false;
    public float _duration;
    public LittleFire(float duration)
    {
        _duration = duration;
 
    }

    private void Awake()
    {
        _animator = GetComponent<Animator>();
    }
    private void Update()
    {
        _timer += Time.deltaTime;
        if (_timer > _duration)
        {
            _animator.SetTrigger("destroy");
        }
    }
    public void SetDuration(float duration)
    {
        _duration = duration;
    }
}
