using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class CollisionPadtile : MonoBehaviour
{
    [SerializeField] float _range;
    [SerializeField] GameObject _warningPrefab;

    protected SlimeBehaviour _slime;
    protected float _damage;

    protected float timer = 0f;
    protected bool _canAttack = false;
    public float _warningTime;
    protected GameObject warning;
    protected GameObject child;
    protected PolygonCollider2D collider;
    public void Init(Vector3 targetPosition, SlimeBehaviour shooter)
    {
        _slime = shooter;
        transform.position = targetPosition;
        _damage = shooter.AttackPower.Value;
        warning = Instantiate(_warningPrefab, transform.position, Quaternion.identity);
        collider = GetComponent<PolygonCollider2D>();
    }
    private void Update()
    {
        if (timer < _warningTime)
            timer += Time.deltaTime;
        else
        {
            _canAttack = true;
            Destroy(warning.gameObject);
            child = transform.GetChild(0).gameObject;
            child.SetActive(true);
            collider.enabled = true;
        }
    } 
    private void OnCollisionEnter2D(Collision2D collision)
    {
        var player = collision.collider.GetComponent<PlayerBehaviour>();
        var flower = collision.collider.GetComponent<Flower>();
        if (player != null)
        {
            player.OnHitted(_damage, transform.position, false);
            Destroy(gameObject, 0.38f);
        }
        else if (flower != null)
        {

            flower.OnHittedBySlime(_slime, _damage);
            Die();
        }
    }
    protected void Die()
    {
        Destroy(gameObject);
    }

}
