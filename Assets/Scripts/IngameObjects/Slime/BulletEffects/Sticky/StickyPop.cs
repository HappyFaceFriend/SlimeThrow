using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StickyPop : MonoBehaviour
{
    private float timer = 0f;
    private Animator _animator;
    public void Awake()
    {
        _animator = GetComponent<Animator>();
    }
    public void Update()
    {
        if (timer < 0.4f)
        {
            timer += Time.deltaTime;
        }
        else
        {
            _animator.SetBool("finish", true);
        }

    }
}
