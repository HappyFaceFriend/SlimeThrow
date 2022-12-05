using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using LitJson;

[System.Serializable]
public class SaveData
{
    public int _round;
    public int _stage;
    public List<string> _upgrades;
    public double _playerHP;
    public double _flowerHP;
    public string _language;
    public double _volume;

    public SaveData(int round, int stage, List<string> upgrades, double playerHP, double flowerHP, string language, double volume)
    {
        this._round = round;
        this._stage = stage;
        this._upgrades = upgrades;
        this._playerHP = playerHP;
        this._flowerHP = flowerHP;
        this._language = language;
        this._volume = volume;
    }
}
