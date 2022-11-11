using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;

public class BubbleGenerate : MonoBehaviour
{
    public GameObject _bubblePrefab;
    public float _moveSpeed;
    float _timer = 0;
    private void Update()
    {
        float xoffset = Random.Range(-1f, 1f);
        float yoffset = Random.Range(-1f, 1f);
        Vector3 position = transform.position + new Vector3(xoffset, yoffset, 0);
        if (_timer < 0.5f)
            _timer += Time.deltaTime;
        else
        {
            GameObject bubble = Instantiate(_bubblePrefab,position, Quaternion.identity);
            Destroy(bubble, 1f);
            _timer = 0;
        }
    }
 
}
