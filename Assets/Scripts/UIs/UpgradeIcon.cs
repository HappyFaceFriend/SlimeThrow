using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UpgradeIcon : MonoBehaviour
{
    [SerializeField] Image _frame;
    [SerializeField] Image _icon;

    [SerializeField] Sprite[] _frameImages;

    public void Init(UpgradeData data)
    {
        _frame.sprite = _frameImages[(int)data.Rarity];
        _icon.sprite = data.Icon;
    }
}
