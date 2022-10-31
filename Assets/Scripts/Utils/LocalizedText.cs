using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class LocalizedText : MonoBehaviour
{
    private TextMeshProUGUI textvariable;
    bool changed = false;
    void Awake()
    {
        textvariable = GetComponent<TextMeshProUGUI>();
    }
    void OnEnable()
    {
        try
        {
            textvariable.text = GlobalDataManager.Instance.GetLocalizedString(textvariable.text);
            changed = true;
        }
        catch { print("fail"); }
    }
    private void Update()
    {
        if (!changed)
        {
            textvariable.text = GlobalDataManager.Instance.GetLocalizedString(textvariable.text);
            changed = true;
        }
    }
    public void Refresh()
    {
        try
        {
            textvariable.text = GlobalDataManager.Instance.GetLocalizedString(textvariable.text);
            changed = true;
        }
        catch { print("fail"); }
    }
}
