using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UpgradePanel : MonoBehaviour
{
    [SerializeField] UpgradeButton[] _buttons;

    public bool IsOpen { get { return gameObject.activeSelf; } }

    public void SetUpgrades(UpgradeData [] datas)
    {
        for(int i=0; i < _buttons.Length; i++)
        {
            _buttons[i].ApplyUIs(datas[i]);
        }
    }
    public void Open()
    {
        gameObject.SetActive(true);
    }
    public void Close()
    {
        gameObject.SetActive(false);
    }
}
