using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PooledParticle : PooledObject
{
    Vector3 _velocity = Vector3.zero;
    float _duration;
    float _eTime = 0f;
    void Awake()
    {
        _duration = GetComponent<ParticleSystem>().main.duration;
    }

    private void OnEnable()
    {
        _eTime = 0f;
    }
    public void SetVelocity(Vector3 velocity)
    {
        _velocity = velocity;
    }

    void Update()
    {
        transform.position += _velocity * Time.deltaTime;
           
        _eTime += Time.deltaTime;
        if(_eTime >= _duration)
        {
            ReturnToPool();
        }
    }
}
