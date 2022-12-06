using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GlobalDataManager : SingletonBehaviour<GlobalDataManager>
{
    TextAsset stringsFile1;

    List<Dictionary<string, string>> localizedUpgradeData;

    private bool _load = false;
    public bool Load { get { return _load; } }

    new void Awake()
    {
        base.Awake();
        stringsFile1 = GlobalRefs.UpgradeManager.UpgradeData;
        localizedUpgradeData = FileUtils.ParseTSV(stringsFile1.text);
    }

    public void SetLoad()
    {
        _load = true;
    }

    public string GetLocalizedString(string string_ko)
    {
        string code = SaveDataManager.Instance.Language;
        var languageData = localizedUpgradeData.Find(x => x["ko"] == string_ko);
        return languageData[code];
    }
}
