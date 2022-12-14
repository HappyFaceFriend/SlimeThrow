using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class PoisonPadtile : SlimePadtile
{
    public float _buffProbability;
    public float _duration;
    public int _damagePerTick;
    float _nextDamageTime;
    bool _buffed = false;
    public GameObject _buffPrefab;
    public new void Init(Vector3 targetPosition, SlimeBehaviour shooter)
    {
        base.Init(targetPosition, shooter);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        var target = collision.GetComponent<PlayerBehaviour>();
        if (target != null)
        {
            _active = true;
            gameObject.GetComponent<CircleCollider2D>().enabled = false;
            gameObject.GetComponent<PolygonCollider2D>().enabled = true;
            transform.GetChild(0).gameObject.SetActive(true);
            transform.GetChild(1).gameObject.SetActive(false);
            if (Random.Range(0f, 1f) <= _buffProbability && !_buffed)
            {
                target.ApplyBuff(new PlayerBuffs.PlayerPoisoned(_duration, _damagePerTick, 1f, _buffPrefab));
                _buffed = true;
            }
        }
    }

    private void OnCollisionStay2D(Collision2D collision)
    {
        if (_active)
        {
            var player = collision.collider.GetComponent<PlayerBehaviour>();
            if (player != null && player.IsTargetable)
            {
                if (_canDamage)
                {
                    player.OnHitted(_damage, transform.position, false);
                    _canDamage = false;
                    if (Random.Range(0f, 1f) <= _buffProbability && !_buffed)
                    {
                        player.ApplyBuff(new PlayerBuffs.PlayerPoisoned(_duration, _damagePerTick, 1f, _buffPrefab));
                        _buffed = true;
                    }
                }
            }
        }
    }

}
