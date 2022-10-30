using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using static UnityEngine.GraphicsBuffer;

public class ParabolaProjectTile : MonoBehaviour
{
    [SerializeField] float _range;
    [SerializeField] float _moveSpeed;

    Vector3 _start;
    Vector3 _end;
    float _movedDistance = 0f;
    float _distance;
    SlimeBehaviour _slime;
    private SpriteRenderer _spriteRenderer;
    float _damage;

    public float GetAngle(Vector3 start, Vector3 end)
    {
        Vector3 v = end - start;
        return Mathf.Atan2(v.y, v.x) * Mathf.Rad2Deg;
    }
    public void Init(Vector3 targetPosition, SlimeBehaviour shooter)
    {
       _start = transform.position;
       _end = targetPosition;
        _distance = _end.x - _start.x;
    }

    public void Update()
    {
        float z = 0;
        Vector3 originalShadowPos = transform.position;

        float totalDistance = (_end - originalShadowPos).magnitude;
        float movedDistance = 0;
        Vector3 moveDir = (_end - originalShadowPos) / totalDistance;

        Vector3 lastShadowPos = originalShadowPos;

        while (true)
        {
            Vector3 shadowPos = lastShadowPos + moveDir * _moveSpeed * Time.deltaTime;
            movedDistance += _moveSpeed * Time.deltaTime;
            if (movedDistance <= totalDistance / 2f)
                z = Mathf.Lerp(0, 5,
                    Utils.Curves.EaseOut(movedDistance / (totalDistance / 2)));
            else
                z = Mathf.Lerp(5, 0,
                    Utils.Curves.EaseIn((movedDistance - totalDistance / 2) / (totalDistance / 2)));

            transform.position = shadowPos + new Vector3(0, z, 0);
            if (Utils.Vectors.IsPositionCrossed(_end, shadowPos, lastShadowPos))
                break;

            lastShadowPos = shadowPos;
        }
        Destroy(gameObject, 1f);
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        var target = collision.collider.GetComponent<IAttackableBySlime>();
        if (target != null)
        {
            target.OnHittedBySlime(_slime, _damage);
            Die();
        }
    }
    void Die()
    {
        Destroy(gameObject);
    }
    public Quaternion LookAt2D(Vector2 forward)
    {
        return Quaternion.Euler(0, 0, Mathf.Atan2(forward.y, forward.x) * Mathf.Rad2Deg);
    }
}
