using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletLandEffect : MonoBehaviour
{
    int _damage;
    public void Init(int damage)
    {
        _damage = damage;
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        print(collision.GetComponent<SlimeBehaviour>());
        SlimeBehaviour slime = collision.transform.GetComponent<SlimeBehaviour>();
        if (slime != null)
        {
            slime.OnHittedByBullet(_damage);
        }
    }
}
