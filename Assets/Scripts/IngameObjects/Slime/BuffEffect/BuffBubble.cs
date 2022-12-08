using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuffBubble : BuffEffectBase
{
    public GameObject _bubblePrefab;
    float _timer = 0;
    float _genTime = 0f;
    public float _xoffset = 1f;

    float _duration;
    private void Update()
    {
        _timer += Time.deltaTime;
      ;
        float xoffset = Random.Range(-_xoffset, _xoffset);

        Vector3 position = transform.position + new Vector3(xoffset, 0.5f, 0);
       if(_timer  < _duration)
        {
            if (_genTime < 0.5f)
                _genTime += Time.deltaTime;
            else
            {
                GameObject bubble = Instantiate(_bubblePrefab, position, Quaternion.identity);
                Destroy(bubble.gameObject, 1f);
                _genTime = 0;
            }
        }
    }
    public void SetDuration(float duration)
    {
        _duration = duration;   

    }
}
