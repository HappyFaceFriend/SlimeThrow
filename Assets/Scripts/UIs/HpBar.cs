using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

using TMPro;

public class HpBar : MonoBehaviour
{
    [SerializeField] Slider _slider;
    [SerializeField] RectTransform _sliderBackEffect;
    [SerializeField] float effectSpeed;
    [SerializeField] TextMeshProUGUI _text;
    [SerializeField] float shakeDuration;
    [SerializeField] float shakeMagnitude;

    RectTransform _rectTransform;

    int _oldHp = -1;

    Vector3 _originalPos;
    Vector3 _offset;
    private void Awake()
    {
        _rectTransform = GetComponent<RectTransform>();
        _originalPos = _rectTransform.localPosition;
        _offset = Vector3.zero;
    }
    public void SetHp(int currentHp, int maxHp)
    {
        _slider.maxValue = maxHp;
        _slider.value = currentHp;

        if (_text != null)
            _text.text = currentHp.ToString() + "/" + maxHp.ToString();

        if (_oldHp == -1)
            _oldHp = currentHp;
        else
        {
            if (_oldHp > currentHp)
                StartCoroutine(Shake());
            _oldHp = currentHp;
        }
    }
    IEnumerator Shake()
    {
        float eTime = 0f;
        float magnitude = shakeMagnitude;
        float duration = shakeDuration;
        while (eTime < duration)
        {
            eTime += Time.unscaledDeltaTime;
            _offset = Utils.Vectors.Vec2ToVec3(Utils.Random.RandomUnitVector2()) * magnitude * (eTime / duration);
            yield return null;
        }
        _offset = Vector3.zero;
    }
    private void LateUpdate()
    {
        if(_sliderBackEffect!= null)
        {
            _sliderBackEffect.anchorMax = Vector2.Lerp(_sliderBackEffect.anchorMax, new Vector2(_slider.normalizedValue, 1), effectSpeed * Time.deltaTime);
            if (Mathf.Abs(_sliderBackEffect.anchorMax.x - _slider.normalizedValue) < 0.02f)
                _sliderBackEffect.anchorMax = new Vector2(_slider.normalizedValue, 1);
        }
        _rectTransform.localPosition = _originalPos + _offset;
    }

}
