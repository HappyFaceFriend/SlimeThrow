using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StickElement : MonoBehaviour
{
    [SerializeField] SlimeBehaviour _slime;
    protected float _damage;
    protected float _elapsedTime = 0f;
    bool _hitted = false;

    private void OnCollisionEnter2D(Collision2D collision)
    {
        var target = collision.collider.GetComponent<PlayerBehaviour>();
        if (target != null)
        {
            if(!_hitted)
            {
                _hitted = true;
                target.OnHitted(_damage, target.transform.position, false);
            }
        }
    }
    protected virtual void Die()
    {
        Destroy(gameObject);
    }
}
