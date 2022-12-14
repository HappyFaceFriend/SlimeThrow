using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SlimeJumptile : MonoBehaviour
{

    SlimeBehaviour _slime;
    private CircleCollider2D _collider;
    float _damage;
    
    public void Init(Vector3 targetPosition, SlimeBehaviour shooter)
    {
        _slime = shooter;
        _damage = shooter.AttackPower.Value;
    }

    public void Update()
    {

    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        var target = collision.collider.GetComponent<PlayerBehaviour>();
        if (target != null)
        {
            target.OnHittedBySlime(_slime, _damage, transform.position);
        }
    }
    void Die()
    {
        Destroy(gameObject);
    }
}
