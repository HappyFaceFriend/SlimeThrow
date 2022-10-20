using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class UpgradeButton : MonoBehaviour
{
    [Header("Test")]
    [SerializeField] UpgradeData _data;

    [Header("References")]
    [SerializeField] TextMeshProUGUI _nameText;
    [SerializeField] TextMeshProUGUI _bodyText;
    [SerializeField] Image _iconImage;

    private void Start()
    {
        if (_data != null)
            ApplyUIs(_data);
    }
    public void OnClick()
    {

    }
    public void ApplyUIs(UpgradeData data)
    {
        _data = data;
        _nameText.text = data.Name;
        _bodyText.text = data.Body;
        _iconImage.sprite = data.Icon;
    }
}
