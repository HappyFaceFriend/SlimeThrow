using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class ButtonSoundPlayer : MonoBehaviour, IPointerEnterHandler
{
    Button _button;

    public void OnPointerEnter(PointerEventData eventData)
    {
        SoundManager.Instance.PlaySFX("ButtonHover", 0.5f);
    }

    private void Awake()
    {
        _button = GetComponent<Button>();
        _button.onClick.AddListener(OnClick);
    }

    void OnClick()
    {
        SoundManager.Instance.PlaySFX("ButtonClick", 0.7f);
    }


}
