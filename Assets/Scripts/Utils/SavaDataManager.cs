using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using LitJson;
using Newtonsoft.Json.Linq;

public class SaveDataManager : SingletonBehaviour<SaveDataManager>
{
    string _language = "en";
    float _volume = 0.5f;
    JsonData Data;

    public void UpdateLanguage()
    {
        if (_language == "ko")
            _language = "en";
        else
            _language = "ko";
    }

    public string Language { get { return _language; } set { _language = value; } }
    public float Volume { get { return _volume; } set { _volume = value; } }

    public void SaveSettings()
    {
        SaveData data = Load();
        if (data != null)
        {
            SaveData newdata = new SaveData(0, 0, null, 0, 0, SaveDataManager.Instance.Language, (double)SaveDataManager.Instance.Volume);
        }
        else
        {
            SaveData newData = new SaveData(data._round, data._stage, data._upgrades, data._playerHP, data._flowerHP, SaveDataManager.Instance.Language, (double)SaveDataManager.Instance.Volume);
        }
    }

    public void Save(SaveData data)
    {
        JsonData jsonData = JsonMapper.ToJson(data);
        System.IO.File.WriteAllText(Application.persistentDataPath + @"\data.json", jsonData.ToString());
    }

    public SaveData Load()
    {
        if (File.Exists(Application.persistentDataPath + @"\data.json"))
        {
            try
            {
                string jsonString = System.IO.File.ReadAllText(Application.persistentDataPath + @"\data.json");
                var jobject = JObject.Parse(jsonString);
                SaveData data = JsonUtility.FromJson<SaveData>(jobject.ToString());
                return data;
            }
            catch (JsonException je)
            {
                Debug.Log("File Format Wrong");
            }
            return null;
        }
        else
        {
            Debug.Log("File Not Found");
            return null;
        }
    }
}