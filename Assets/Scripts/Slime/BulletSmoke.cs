using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletSmoke : MonoBehaviour
{
    SpriteRenderer _spriteRenderer;
    public void Awake()
    {
        _spriteRenderer = GetComponent<SpriteRenderer>();
    }

    public void SetColor(string color)
    {
        if (name.Equals("Fire "))
        {
            Debug.Log("빨간색으로 바꾼다");
            _spriteRenderer.color = new Color(1, 0, 0, 1);
        }
        else if (name == "Ice")
            _spriteRenderer.color = new Color(0, 0, 0, 1);
        else
            Debug.Log("맞는 색이 없다");
    }    
}
