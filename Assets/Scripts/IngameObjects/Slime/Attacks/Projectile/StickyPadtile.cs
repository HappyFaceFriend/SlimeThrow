using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;


public class StickyPadtile : SlimePadtile
{
    public float _duration;
    public float _slowPercent;
    bool _buffed = false;

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
            if(!_buffed)
            {
                target.ApplyBuff(new PlayerBuffs.PlayerSlow(_duration, _slowPercent));
                _buffed = true;
            }

        }
    }

    private void OnCollisionStay2D(Collision2D collision)
    {
        if (_active)
        {
            var player = collision.collider.GetComponent<PlayerBehaviour>();
            var flower = collision.collider.GetComponent<Flower>();
            if (player != null)
            {
                if (_canDamage)
                {
                    player.OnHitted( _damage, transform.position, false);
                    _canDamage = false;
                }
            }
            else if (flower != null)
            {
                if (_canDamage)
                {
                    flower.OnHittedBySlime(_slime, _damage);
                    _canDamage = false;
                }
            }
            if (!_buffed)
            {
                player.ApplyBuff(new PlayerBuffs.PlayerSlow(_duration, _slowPercent));
                _buffed = true;
            }

        }
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        if (_active)
        {
            var player = collision.collider.GetComponent<PlayerBehaviour>();
            if (player != null)
            {

            }
        }
    }

}
