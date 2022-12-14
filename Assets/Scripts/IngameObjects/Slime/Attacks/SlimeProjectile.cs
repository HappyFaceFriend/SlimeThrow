using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SlimeProjectile : MonoBehaviour
{
    [SerializeField] float _range;
    [SerializeField] float _moveSpeed;

    protected Vector3 _moveDir = Vector3.zero;
    float _movedDistance = 0f;
    protected SlimeBehaviour _slime;
    private SpriteRenderer _spriteRenderer;
    protected float _damage;
    protected float _elapsedTime = 0f;

    public float GetAngle(Vector3 start, Vector3 end)
    {
        Vector3 v = end - start;
        return Mathf.Atan2(v.y, v.x) * Mathf.Rad2Deg;
    }
    public virtual void Init(Vector3 targetPosition, SlimeBehaviour shooter)
    {
        _moveDir = (targetPosition - transform.position).normalized;
        _slime = shooter;
        float angle = GetAngle(_moveDir, Vector3.zero) - 180f;
        transform.rotation = Quaternion.AngleAxis(angle, Vector3.forward);
        _damage = shooter.AttackPower.Value;
    }
    public void Init(Vector3 dir, float damage)
    {
        _moveDir = dir;
        float angle = GetAngle(_moveDir, Vector3.zero) - 180f;
        transform.rotation = Quaternion.AngleAxis(angle, Vector3.forward);
        _damage = damage;
    }
    public void Update()
    {
        _elapsedTime += Time.deltaTime;
        transform.position += _moveDir * _moveSpeed * Time.deltaTime;
        _movedDistance += _moveSpeed * Time.deltaTime;
        if (_movedDistance > _range)
        {
            Die();
        }
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        var target = collision.collider.GetComponent<PlayerBehaviour>();
        if (target != null)
        {
            target.OnHitted( _damage, transform.position, true);
            Die();
        }
    }
    protected virtual void Die()
    {
        Destroy(gameObject);
    }
}
