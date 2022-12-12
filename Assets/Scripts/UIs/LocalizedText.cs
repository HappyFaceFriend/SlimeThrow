using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

using TMPro;
public class LocalizedText : MonoBehaviour
{
    Text text2;
    TextMeshProUGUI text;
    string key;

    private void Awake()
    {
        text = GetComponent<TextMeshProUGUI>();
        text2 = GetComponent<Text>();
        if (text != null)
            key = text.text;
        else
            key = text2.text;
    }
    public virtual void Refresh()
    {
        if (text != null)
            text.text = GlobalDataManager.Instance.GetLocalizedTitle(key);
        else
            text2.text = GlobalDataManager.Instance.GetLocalizedTitle(key);
    }
    private void OnEnable()
    {
        Refresh();
    }
}
