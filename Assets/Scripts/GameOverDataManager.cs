using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using TMPro;

public class GameOverDataManager : MonoBehaviour
{
    public class GameOverData
    {
        public int Stage;
        public int SlimeKilled;
        public UpgradeData[] Upgrades;
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


    GameOverData _data;

    public static GameOverData Data { get { return _safeInstance._data; } set { _safeInstance._data = value; } }



}
