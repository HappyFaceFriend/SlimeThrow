using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowTransform : MonoBehaviour
{
    [SerializeField] Transform _followTarget;

    Vector3 _originalOffset;

    private void Awake()
    {
        _originalOffset = transform.position - _followTarget.position;
    }


    private void LateUpdate()
    {
        Vector3 targetPos = _originalOffset + _followTarget.position;
        transform.position = new Vector3(targetPos.x, targetPos.y);
    }
}
