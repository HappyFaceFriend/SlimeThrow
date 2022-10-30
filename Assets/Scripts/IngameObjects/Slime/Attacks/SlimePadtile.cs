using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SlimePadtile : MonoBehaviour
{
    [SerializeField] float _range;

    Vector3 _moveDir = Vector3.zero;
    SlimeBehaviour _slime;
    private SpriteRenderer _spriteRenderer;
    float _damage;
    float timer = 0f;

    public float GetAngle(Vector3 start, Vector3 end)
    {
        Vector3 v = end - start;
        return Mathf.Atan2(v.y, v.x) * Mathf.Rad2Deg;
    }
    public void Init(Vector3 targetPosition, SlimeBehaviour shooter)
    {
        _slime = shooter;
        transform.position = targetPosition;
        _damage = shooter.AttackPower.Value;
    }

    public void Update()
    {
        if(timer < 2f)
        {
            timer += Time.deltaTime;
        }
        else
        {
            Die();
        }
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        var player = collision.collider.GetComponent<PlayerBehaviour>();
        var flower = collision.collider.GetComponent<Flower>();
        if (player != null)
        {
            player.OnHittedByPad(_slime, _damage);
            Die();
        }
        else if (flower != null)  
        {
            flower.OnHittedBySlime(_slime, _damage);
            Die();
        }
    }
    void Die()
    {
        Destroy(gameObject, 2f);
    }
  
}