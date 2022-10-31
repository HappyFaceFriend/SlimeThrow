using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SlimeJumptile : MonoBehaviour
{
    [SerializeField] float _range;
    public float _animationTime;

    SlimeBehaviour _slime;
    private CircleCollider2D _collider;
    float _damage;
    float _changeSpeed;
    
    public void Init(Vector3 targetPosition, SlimeBehaviour shooter, CircleCollider2D collider)
    {
        _slime = shooter;
        _damage = shooter.AttackPower.Value;
        _collider = collider;
        _changeSpeed = _range / _animationTime;
    }

    public void Update()
    {
        if (_collider.radius < _range)
            _collider.radius += _changeSpeed * Time.deltaTime;
        else
        {
            _collider.radius = 0.16f;
            Die();
        }
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        var target = collision.collider.GetComponent<PlayerBehaviour>();
        if (target != null)
        {
            target.OnHittedBySlime(_slime, _damage);
        }
    }
    void Die()
    {
        Destroy(gameObject);
    }
}
