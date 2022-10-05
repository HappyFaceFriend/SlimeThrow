using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SlimeMovement : MonoBehaviour
{
    float _moveSpeed;
    Transform _target;
    bool _isMovementDone;
    public bool IsMovementDone { get { return _isMovementDone; } }
    public Vector3 TargetPos { get { return _target.position; } }
    public void OnStartMoving()
    {
        _target = LevelManager.Instance.Flower;
        _isMovementDone = false;
    }
    private void Awake()
    {
        _moveSpeed = GetComponent<SlimeBehaviour>().MoveSpeed;  
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
