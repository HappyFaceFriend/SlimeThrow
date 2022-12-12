using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

using TMPro;
public class LocalizedTitle : LocalizedText
{
    [SerializeField] GameObject _koTitle;
    [SerializeField] GameObject _enTitle;
    private void Awake()
    {
    }
    public override void Refresh()
    {
        if(SaveDataManager.Instance.Language == "ko")
        {
            _koTitle.SetActive(true);
            _enTitle.SetActive(false);
        }
        else
        {
            _koTitle.SetActive(false);
            _enTitle.SetActive(true);
        }
    }
    private void OnEnable()
    {
        Refresh();
    }
}
