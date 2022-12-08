using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class EndingPlayer : MonoBehaviour
{
    [SerializeField] float rotateSpeed;

    private void Update()
    {
        transform.rotation = Quaternion.Euler(0, 0, transform.rotation.eulerAngles.z + rotateSpeed * Time.deltaTime);
    }
}
