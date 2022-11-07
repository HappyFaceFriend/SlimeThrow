using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BubbleBehaviour : MonoBehaviour
{
    public float _moveSpeed;
    void Update()
    {
        transform.position += new Vector3(0, _moveSpeed * Time.deltaTime, 0); 
    }
}
