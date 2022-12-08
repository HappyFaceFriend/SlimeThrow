using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using LitJson;

[System.Serializable]
public class SaveData
{
    public int _stage;
    public List<string> _upgrades;
    public double _playerHP;
    public double _flowerHP;
    public int _slimesKilled;
    public string _language;
    public double _volume;

    public SaveData(int stage, List<string> upgrades, double playerHP, double flowerHP, int SlimesKilled, string language, double volume)
    {
        this._stage = stage;
        this._upgrades = upgrades;
        this._playerHP = playerHP;
        this._flowerHP = flowerHP;
        this._slimesKilled = SlimesKilled;
        this._language = language;
        this._volume = volume;
    }
}
