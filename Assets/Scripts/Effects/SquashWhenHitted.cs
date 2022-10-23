using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SquashWhenHitted : MonoBehaviour
{
    [SerializeField] Vector2 _squashScale;
    [SerializeField] float _duration;

    Vector3 _originalScale = new Vector3(1, 1, 1);
    Coroutine _squashCoroutine;
    public void Squash()
    {
        if (_squashCoroutine != null)
            StopCoroutine(_squashCoroutine);
        _squashCoroutine = StartCoroutine(SquashCoroutine());
    }

    IEnumerator SquashCoroutine()
    {
        float eTime = 0f;
        Vector2 sign;
        while (eTime < _duration)
        {
            yield return null;
            Vector2 currentSquash = Vector2.Lerp(_originalScale, _squashScale, SquashCurve(eTime / _duration));
            sign = new Vector2(transform.localScale.x > 0 ? 1 : -1, transform.localScale.y > 0 ? 1 : -1);
            transform.localScale = new Vector3(currentSquash.x * sign.x, currentSquash.y * sign.y, 1);
            eTime += Time.deltaTime;
        }
        sign = new Vector2(transform.localScale.x > 0 ? 1 : -1, transform.localScale.y > 0 ? 1 : -1);
        transform.localScale = new Vector3(_originalScale.x * sign.x, _originalScale.y * sign.y, 1);
    }
    float SquashCurve(float t)
    {
        float max = 0.3f;
        if (t <= max)
            return (t / max);
        else
            return 1 - (t - max) / (1 - max);
    }


}
