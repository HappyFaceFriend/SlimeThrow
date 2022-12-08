﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KingRockProjectile : SlimeProjectile
{
    [SerializeField] GameObject _landEffectPrefab;
    public float _duration;
    public float _probability;

    public new void Init(Vector3 targetPosition, SlimeBehaviour shooter)
    {
        base.Init(targetPosition, shooter);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        var target = collision.collider.GetComponent<PlayerBehaviour>();  // 꽃 공격하는 건 어떠카지
        if (target != null)
        {
            target.OnHitted(_damage, this.transform.position, true);
            if(_slime.IsFever())
            {
                if(Random.Range(0f, 1f) <= _probability)
                    target.ApplyBuff(new PlayerBuffs.PlayerStun(_duration));
            }
            Die();
        }
    }
    private void OnDestroy()
    {
        if (!gameObject.scene.isLoaded)
            return;
        GameObject effect = Instantiate(_landEffectPrefab, transform.position, Quaternion.identity);
        Destroy(effect.gameObject, 3.0f);
    }
    public void SetProb(float prob)
    {
        _probability = prob;
    }

}
