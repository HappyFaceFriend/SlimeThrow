using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class ElectroPadtile : CollisionPadtile
{
    public float _buffProbability;
    public float _duration;
    public void Init(Vector3 targetPosition, SlimeBehaviour shooter)
    {
        base.Init(targetPosition, shooter);
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        var player = collision.collider.GetComponent<PlayerBehaviour>();
        var flower = collision.collider.GetComponent<Flower>();
        if (player != null)
        {
            player.OnHittedBySlime(_slime, _damage);
            player.ApplyBuff(new PlayerBuffs.PlayerStun(_duration, _buffProbability));
            Destroy(gameObject, 0.38f);
        }
        else if (flower != null)
        {

            flower.OnHittedBySlime(_slime, _damage);
            Die();
        }
    }
}
