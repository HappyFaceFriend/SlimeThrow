using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

using TMPro;

public class HpBar : MonoBehaviour
{
    [SerializeField] Slider _slider;
    [SerializeField] TextMeshProUGUI _text;

    public void SetHp(int currentHp, int maxHp)
    {
        _slider.maxValue = maxHp;
        _slider.value = currentHp;

        if (_text != null)
            _text.text = currentHp.ToString() + "/" + maxHp.ToString();
    }

}
