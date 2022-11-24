using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LittleFire : MonoBehaviour
{
    Animator _animator;
    //SpriteRenderer _spriteRenderer;
    float _timer = 0;
    bool _finish = false;
    bool _looping = false;
    float _duration;
    float _flashTime;
    private void Awake()
    {
        _animator = GetComponent<Animator>();
    }
    private void Update()
    {
        _timer += Time.deltaTime;
        if (_timer > 0.1f && !_looping)
        {
            _looping = true;
            _animator.SetBool("active", true);
        }
        if (_timer > _duration)
        {
            _animator.SetBool("active", false);
        }
    }
    public void SetDuration(float duration)
    {
        _duration = duration;
    }
}
