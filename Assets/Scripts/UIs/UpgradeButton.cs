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
    [SerializeField] UpgradePanel _panel;
    [SerializeField] TextMeshProUGUI _nameText;
    [SerializeField] TextMeshProUGUI _bodyText;
    [SerializeField] Image _iconImage;

    string _currentLanguage;

    private void Start()
    {
        if (_data != null)
            ApplyUIs(_data);
        _currentLanguage = SaveDataManager.Instance.GetLanguage;
    }
    public void OnClick()
    {
        GlobalRefs.UpgradeManager.AddUpgrade(_data);
        _panel.Close();
    }
    public void ApplyUIs(UpgradeData data)
    {
        _data = data;
        _nameText.text = data.Name;
        LocalizeText(_nameText);
        _bodyText.text = data.Body;
        LocalizeText(_bodyText);
        _iconImage.sprite = data.Icon;
        _currentLanguage = SaveDataManager.Instance.GetLanguage;
    }

    private void Update()
    {
        if(_currentLanguage != SaveDataManager.Instance.GetLanguage)
        {
            LocalizeText(_nameText);
            LocalizeText(_bodyText);
        }
    }

    public void LocalizeText(TextMeshProUGUI _Text)
    {
        try
        {
            _Text.text = GlobalDataManager.Instance.GetLocalizedString(_Text.text);
        }
        catch { print("fail"); }
    }
}
