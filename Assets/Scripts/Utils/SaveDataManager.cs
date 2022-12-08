using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using LitJson;
using Newtonsoft.Json.Linq;

using UnityEngine.SceneManagement;

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

    public string Language { get { return _language; } set { _language = value; TranslateAllTextsInScene(); } }
    public float Volume { get { return _volume; } set { _volume = value; } }

    void TranslateAllTextsInScene()
    {
        for(int i=0; i<SceneManager.sceneCount; i++)
        {
            List<GameObject> rootObjectsInScene = new List<GameObject>();
            Scene scene = SceneManager.GetSceneAt(i);
            scene.GetRootGameObjects(rootObjectsInScene);

            foreach(GameObject root in rootObjectsInScene)
            {
                LocalizedText[] allComponents = root.GetComponentsInChildren<LocalizedText>(true);
                for (int j = 0; j < allComponents.Length; j++)
                {
                    allComponents[j].Refresh();
                }
            }
        }
    }
    public void SaveSettings()
    {
        SaveData data = Load();
        if (data == null)
        {
            SaveData newdata = new SaveData(0, null, 0, 0, 0, SaveDataManager.Instance.Language, (double)SaveDataManager.Instance.Volume, 1);
            Save(newdata);
        }
        else
        {
            SaveData newData = new SaveData(data._stage, data._upgrades, data._playerHP, data._flowerHP, data._slimesKilled, SaveDataManager.Instance.Language, (double)SaveDataManager.Instance.Volume, data._rerollCount);
            Save(newData);
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