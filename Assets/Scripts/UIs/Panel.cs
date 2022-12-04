using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Panel : MonoBehaviour
{
    Animator _animator;

    private void Awake()
    {
        _animator = GetComponent<Animator>();
    }
    public void Close()
    {
        _animator.SetTrigger("Close");
    }

    public void AnimEvent_Close()
    {
        gameObject.SetActive(false);
    }
}
