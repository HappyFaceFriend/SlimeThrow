using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlipObjectToPoint : MonoBehaviour
{
    public Vector2 TargetPoint { get; set; }

    void Update()
    {
        if (TargetPoint.x > transform.position.x)
        {
            transform.localScale = new Vector3(Mathf.Abs(transform.localScale.x), 1, 1);
        }
        else if (TargetPoint.x < transform.position.x)
        {
            transform.localScale = new Vector3(-Mathf.Abs(transform.localScale.x), 1, 1);
        }
    }
}