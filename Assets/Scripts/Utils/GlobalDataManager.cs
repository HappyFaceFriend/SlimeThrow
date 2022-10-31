using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GlobalDataManager : SingletonBehaviour<GlobalDataManager>
{
    [SerializeField] TextAsset stringsFile;

    List<Dictionary<string, string>> localizedStringData;

    new void Awake()
    {
        localizedStringData = FileUtils.ParseTSV(stringsFile.text);
    }

    public string GetLocalizedString(string string_en)
    {
        string code = SavaDataManager.Instance.GetLanguage;
        var languageData = localizedStringData.Find(x => x["en"] == string_en);
        print(code);
        if (languageData == null)
            return string_en;
        return languageData[code];
    }
}
