using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SlimeMovement : MonoBehaviour
{
    [SerializeField] private float _moveSpeed;
    private Transform _target;
    private bool _isMovementDone;
    public bool IsMovementDone { get { return _isMovementDone; } }
    public void OnStartMoving()
    {
        _target = LevelManager.Instance.Flower;
        _isMovementDone = false;
    }
    public void OnUpdate()
    {
        Vector3 moveDir = (_target.position - transform.position).normalized;
        transform.position += moveDir * _moveSpeed * Time.deltaTime;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.transform == _target)
        {
            _isMovementDone = true;
        }
    }
}
