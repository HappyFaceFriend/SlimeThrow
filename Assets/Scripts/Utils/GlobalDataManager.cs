using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GlobalDataManager : SingletonBehaviour<GlobalDataManager>
{
    [SerializeField] TextAsset stringsFile1;
    [SerializeField] TextAsset stringsFile2;

    List<Dictionary<string, string>> localizedUpgradeData;
    List<Dictionary<string, string>> localizedTitleData;


    List<Dictionary<string, string>> LocalizedUpgradeData { get 
        { 
            if(localizedUpgradeData == null)
                localizedUpgradeData = FileUtils.ParseTSV(stringsFile1.text);
            return localizedUpgradeData; 
        } }
    List<Dictionary<string, string>> LocalizedTitleData
    {
        get
        {
            if (localizedTitleData == null)
                localizedTitleData = FileUtils.ParseTSV(stringsFile2.text);
            return localizedTitleData;
        }
    }

    private bool _load = false;
    public bool Load { get { return _load; } }

    new void Awake()
    {
        base.Awake();
        localizedUpgradeData = FileUtils.ParseTSV(stringsFile1.text);
        localizedTitleData = FileUtils.ParseTSV(stringsFile2.text);
    }

    public void SetLoad()
    {
        _load = true;
    }

    public string GetLocalizedString(string string_ko)
    {
        string code = SaveDataManager.Instance.Language;
        var languageData = LocalizedUpgradeData.Find(x => x["ko"] == string_ko);
        return languageData[code];
    }

    public string GetLocalizedTitle(string string_ko)
    {
        string code = SaveDataManager.Instance.Language;
        var languageData = LocalizedTitleData.Find(x => x["ko"] == string_ko);
        return languageData[code];
    }
}
