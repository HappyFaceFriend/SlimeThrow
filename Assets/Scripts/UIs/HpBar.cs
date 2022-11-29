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

    public void SetHp(int currentHp, int maxHp)
    {
        _slider.maxValue = maxHp;
        _slider.value = currentHp;

        if (_text != null)
            _text.text = currentHp.ToString() + "/" + maxHp.ToString();
    }

    private void LateUpdate()
    {
        if(_sliderBackEffect!= null)
        {
            _sliderBackEffect.anchorMax = Vector2.Lerp(_sliderBackEffect.anchorMax, new Vector2(_slider.normalizedValue, 1), effectSpeed * Time.deltaTime);
            if (Mathf.Abs(_sliderBackEffect.anchorMax.x - _slider.normalizedValue) < 0.02f)
                _sliderBackEffect.anchorMax = new Vector2(_slider.normalizedValue, 1);
        }
    }

}
