using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

using TMPro;

public class UpgradePanel : Panel
{
    [SerializeField] UpgradeButton[] _buttons;
    [SerializeField] Button _rerollButton;
    [SerializeField] Image _rerollTag;
    [SerializeField] TextMeshProUGUI _rerollButtonText;

    public bool IsOpen { get { return gameObject.activeSelf; } }

    public void SetUpgrades(UpgradeData [] datas)
    {
        for(int i=0; i < _buttons.Length; i++)
        {
            _buttons[i].ApplyUIs(datas[i]);
        }
        ApplyUIs();
    }

    void ApplyUIs()
    {
        _rerollButtonText.text = GlobalRefs.UpgradeManager.RerollCount.ToString();
        Color color = _rerollTag.color;
        if (GlobalRefs.UpgradeManager.RerollCount == 0)
        {
            _rerollTag.color = new Color(color.r, color.g, color.b, 0.75f);
            _rerollButton.interactable = false;
        }
        else
        {
            _rerollTag.color = new Color(color.r, color.g, color.b, 1);
            _rerollButton.interactable = true;
        }
    }
    public void Open()
    {
        ApplyUIs();
        gameObject.SetActive(true);
    }
}
