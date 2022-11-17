using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SavaDataManager : SingletonBehaviour<SavaDataManager>
{
    [SerializeField]string _language;

    public string GetLanguage { get { return _language; } }

    public void UpdateLanguage()
    {
        if (_language == "ko")
            _language = "en";
        else
            _language = "ko";
    }
}
