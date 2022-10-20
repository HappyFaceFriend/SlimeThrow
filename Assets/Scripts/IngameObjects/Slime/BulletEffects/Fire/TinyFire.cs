using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TinyFire : MonoBehaviour
{
    public Vector3 _horizontal;
    private Vector3 _vertical;
    private float timer = 0f;
    private Animator _animator;
    public void Awake()
    {
        _animator = GetComponent<Animator>();
        _horizontal = Vector3.right;
        _vertical = Vector3.up;
    }
    public void Update()
    {
        if(timer < 1f)
        {
            _vertical.y += -3.0f * Time.deltaTime;
            transform.position += _vertical * Time.deltaTime;
            transform.position += _horizontal * Time.deltaTime;
            timer += Time.deltaTime;
        }
        else
        {
            _vertical = Vector3.zero;
            _horizontal = Vector3.zero;
            _animator.SetBool("finish", true);
        }
        
    }

    public void SetDirection(Vector3 horziontal)
    {
        _horizontal = horziontal;
    }

 
}
