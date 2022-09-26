using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlipObjectToPoint : MonoBehaviour
{
    public Vector2 targetPoint { get; set; }

    void Update()
    {
        if (targetPoint.x > transform.position.x)
        {
            transform.localScale = new Vector3(Mathf.Abs(transform.localScale.x), 1, 1);
        }
        else if (targetPoint.x < transform.position.x)
        {
            transform.localScale = new Vector3(-Mathf.Abs(transform.localScale.x), 1, 1);
        }
    }
}