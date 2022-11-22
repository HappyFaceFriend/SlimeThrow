using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class SlimePadtile : MonoBehaviour
{
    [SerializeField] float _range;
    [SerializeField] float _moveSpeed;
    public float _padDurationTime;

    protected float _elaspsedTime;
    protected float _damage;
    protected SlimeBehaviour _slime;
    protected bool _active = false;
    protected bool _canDamage = true;

    Vector3 _moveDir = Vector3.zero;
    float _movedDistance = 0f;
    float _timer = 0f;
    float _damagetimer = 0f;
    SpriteRenderer _spriteRenderer;



    public float GetAngle(Vector3 start, Vector3 end)
    {
        Vector3 v = end - start;
        return Mathf.Atan2(v.y, v.x) * Mathf.Rad2Deg;
    }
    public void Init(Vector3 targetPosition, SlimeBehaviour shooter)
    {
        _slime = shooter;
        _damage = shooter.AttackPower.Value;
        _moveDir = (targetPosition - transform.position).normalized;
        _spriteRenderer = GetComponentInChildren<SpriteRenderer>(true);
    }

    public void Update()
    {
        _elaspsedTime += Time.deltaTime;
        if (_active == false)
        {
            transform.position += _moveDir * _moveSpeed * Time.deltaTime;
            _movedDistance += _moveSpeed * Time.deltaTime;
            if (_movedDistance > _range)
            {
                transform.GetChild(0).gameObject.SetActive(true);
                transform.GetChild(1).gameObject.SetActive(false);
                _active = true;
            }
        }
        else
        {
            if (_timer < _padDurationTime)
                _timer += Time.deltaTime;
            else
            {
                StartCoroutine("Fadeout");
            }
            if (_damagetimer < 2f)
                _damagetimer += Time.deltaTime;
            else
            {
                _canDamage = true;
                _damagetimer = 0f;
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
                    player.TakeDamage(_damage);
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
        }
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
        }
    }
    protected void Die()
    {
        Destroy(gameObject);
    }
    IEnumerator Fadeout()
    {

        Color color = _spriteRenderer.material.color;
        color.a -= 0.01f;
        Debug.Log(color.a);
        _spriteRenderer.material.color = color;
        if(color.a < 0)
            Destroy(gameObject);
        yield return null;
    }
}

