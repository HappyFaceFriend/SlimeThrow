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

    public SaveData(int round, int stage, List<string> upgrades, double playerHP, double flowerHP)
    {
        this._round = round;
        this._stage = stage;
        this._upgrades = upgrades;
        this._playerHP = playerHP;
        this._flowerHP = flowerHP;
    }
}
