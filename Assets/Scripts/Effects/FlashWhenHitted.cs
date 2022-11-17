using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlashWhenHitted : MonoBehaviour
{
    [SerializeField] Material _white;

    Material _default;
    SpriteRenderer [] _sprites;
    int _frameCount = -1;
    int _frameDuration = -1;
    private void Awake()
    {
        _sprites = GetComponentsInChildren<SpriteRenderer>();
        _default = GetComponentInChildren<SpriteRenderer>().material;
    }

    public void Flash(float duration = 0.1f)
    {
        Unflash();
        foreach (SpriteRenderer sprite in GetComponentsInChildren<SpriteRenderer>())
        {
            sprite.material = _white;
        }
        Invoke("Unflash", duration);
    }
    public void Flash(int frames)
    {
        Unflash();
        foreach (SpriteRenderer sprite in GetComponentsInChildren<SpriteRenderer>())
        {
            sprite.material = _white;
        }
        _frameDuration = frames;
        _frameCount = 0;
    }
    private void Update()
    {
        if(_frameCount >= 0)
            _frameCount++;
        if(_frameDuration >= 0 && _frameCount > _frameDuration)
        {
            Unflash();
        }    
    }

    void Unflash()
    {
        foreach (SpriteRenderer sprite in GetComponentsInChildren<SpriteRenderer>())
        {
            sprite.material = _default;
        }
        _frameDuration = -1;
        _frameCount = -1;
        CancelInvoke();
    }
}
