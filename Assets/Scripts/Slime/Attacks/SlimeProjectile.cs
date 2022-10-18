using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SlimeProjectile : MonoBehaviour
{
    [SerializeField] float _range;
    [SerializeField] float _moveSpeed;

    Vector3 _moveDir = Vector3.zero;
    float _movedDistance = 0f;
    SlimeBehaviour _slime;
    private SpriteRenderer _spriteRenderer;

    public float GetAngle(Vector3 start, Vector3 end)
    {
        Vector3 v = end - start;
        return Mathf.Atan2(v.y, v.x) * Mathf.Rad2Deg;
    }
    public void Init(Vector3 targetPosition, SlimeBehaviour shooter)
    {
        _moveDir = (targetPosition - transform.position).normalized;
        _slime = shooter;
        float angle = GetAngle(_moveDir, Vector3.zero) - 180f;
        transform.rotation = Quaternion.AngleAxis(angle, Vector3.forward);
    }

    public void Update()
    {
        transform.position += _moveDir * _moveSpeed * Time.deltaTime;
        _movedDistance += _moveSpeed * Time.deltaTime;
        if (_movedDistance > _range)
        {
            Die();
        }
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        var target = collision.collider.GetComponent<IAttackableBySlime>();
        if (target != null)
        {
            target.OnHittedBySlime(_slime, _slime.AttackPower);
            Die();
        }
    }
    void Die()
    {
        Destroy(gameObject);
    }
}
