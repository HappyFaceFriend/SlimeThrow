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
        print(SaveDataManager.Instance.Language);
        print(GlobalDataManager.Instance);
        text.text = GlobalDataManager.Instance.GetLocalizedTitle(key);
    }
    private void OnEnable()
    {
        Refresh();
    }
}
