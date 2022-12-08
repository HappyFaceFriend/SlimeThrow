using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using TMPro;
public class LocalizedText : MonoBehaviour
{
    TextMeshProUGUI text;
    string key;

    private void Awake()
    {
        text = GetComponent<TextMeshProUGUI>();
        key = text.text;
    }
    public void Refresh()
    {
        text.text = GlobalDataManager.Instance.GetLocalizedString(key);
    }
    private void OnEnable()
    {
        Refresh();
    }
}
