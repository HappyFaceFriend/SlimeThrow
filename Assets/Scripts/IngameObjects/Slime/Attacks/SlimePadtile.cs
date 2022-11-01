using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class SlimePadtile : MonoBehaviour
{
    [SerializeField] float _range;
    public float _padDurationTime;

    Vector3 _moveDir = Vector3.zero;
    SlimeBehaviour _slime;
    float _damage;
    float _timer = 0f;
    float _damagetimer = 0f;
    bool _canDamage = true;

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
        if(_timer < _padDurationTime)
            _timer += Time.deltaTime;
        else
            Die();
        if(_damagetimer < 2f)
            _damagetimer += Time.deltaTime;
        else
        {
            _canDamage = true;
            _damagetimer = 0f;
        }
    }
    private void OnCollisionStay2D(Collision2D collision)
    {
        var player = collision.collider.GetComponent<PlayerBehaviour>();
        var flower = collision.collider.GetComponent<Flower>();
        if (player != null)
        {
            if(_canDamage)
            {
                player.OnHittedByPad(_slime, _damage);
                _canDamage=false;
            }
        }
        else if (flower != null)  
        {
           if(_canDamage)
            {
                flower.OnHittedBySlime(_slime, _damage);
                _canDamage = false;
            }
        }
    }
    void Die()
    {
        Destroy(gameObject);
    }
  
}
