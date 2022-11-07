using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowTransform : MonoBehaviour
{
    [SerializeField] Transform _followTarget;

    RectTransform _rectTransform;
    [SerializeField] Vector2 _originalOffset;

    private void Awake()
    {
    }


    private void LateUpdate()
    {
        transform.position = Camera.main.WorldToScreenPoint(_followTarget.position) + Utils.Vectors.Vec2ToVec3(_originalOffset);
    }
}
