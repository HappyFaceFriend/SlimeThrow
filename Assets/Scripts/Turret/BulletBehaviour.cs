using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletBehaviour : MonoBehaviour
{
    [SerializeField] float _moveSpeed;
    Vector3 _targetPosition;
    public void StartShoot(Vector3 targetPosition)
    {
        _targetPosition = targetPosition;
    }

    private void Update()
    {
        transform.position = Vector3.Lerp(transform.position, _targetPosition, Time.deltaTime * _moveSpeed);
    }
}
