using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GlobalDataManager : SingletonBehaviour<GlobalDataManager>
{
    [SerializeField] TextAsset stringsFile;

    List<Dictionary<string, string>> localizedStringData;

    private bool _load = false;
    public bool Load { get { return _load; } }


    new void Awake()
    {
        base.Awake();
        localizedStringData = FileUtils.ParseTSV(stringsFile.text);
    }

    public void SetLoad()
    {
        _load = true;
    }

    public string GetLocalizedString(string string_ko)
    {
        string code = SaveDataManager.Instance.GetLanguage;
        var languageData = localizedStringData.Find(x => x["ko"] == string_ko);
        if (languageData == null)
            return string_ko;
        return languageData[code];
    }
}
