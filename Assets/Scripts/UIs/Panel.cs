using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Panel : MonoBehaviour
{
    [SerializeField] Image _fade;
    Animator _animator;

    float _fadeAlpha;

    protected void Awake()
    {
        _animator = GetComponent<Animator>();
        if(_fade != null)
            _fadeAlpha = _fade.color.a;
    }
    protected void OnEnable()
    {
        if (_fade != null)
        {
            StartCoroutine(StartFade(0, _fadeAlpha));
            _fade.gameObject.SetActive(true);
        }
    }
    IEnumerator StartFade(float current, float target)
    {
        float eTime = 0f;
        float duration = 0.2f;

        Color color = _fade.color;
        while (eTime < duration)
        {
            eTime += Time.unscaledTime;
            color.a = Mathf.Lerp(current, target, eTime / duration);
            _fade.color = color;
            yield return null;
        }
        color.a = target;
        _fade.color = color;
    }
    public void Close()
    {
        _animator.SetTrigger("Close");
        if (_fade != null)
            StartCoroutine(StartFade(_fadeAlpha,0));
    }

    public void AnimEvent_Close()
    {
        gameObject.SetActive(false);
        if (_fade != null)
            _fade.gameObject.SetActive(false);
    }
}
