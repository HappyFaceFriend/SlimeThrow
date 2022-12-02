using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using TMPro;

public class GameOverDataManager : MonoBehaviour
{
    public class GameOverData
    {
        public int Stage;
    }
    static GameOverDataManager _instance = null;
    static GameOverDataManager _safeInstance
    {
        get
        {
            if (_instance == null)
                _instance = GameObject.FindGameObjectWithTag("DataManager").GetComponent<GameOverDataManager>();
            return _instance;
        }
    }

    [SerializeField] TextMeshProUGUI _clearedStageText;

    static GameOverData _data;

    public static void SetData(GameOverData gameOverData)
    {
        _data = gameOverData;
    }

    private void Awake()
    {
        _safeInstance.UpdatePanel();
    }

    void UpdatePanel()
    {
        if(_data != null)
        {
            _clearedStageText.text =  " stage " + _data.Stage.ToString();
        }
    }
}
