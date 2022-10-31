using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SavaDataManager : SingletonBehaviour<SavaDataManager>
{
    [SerializeField]string _language;
    string _currentLanguage;

    public string GetLanguage { get { return _currentLanguage; } }

    new void Awake()
    {
        _currentLanguage = _language;
        if (_currentLanguage == null)
            print("no language setting");
    }

    private void Update()
    {
        if (_currentLanguage != _language)
            _currentLanguage = _language;
    }
}
