using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GlobalRefs : MonoBehaviour
{
    [SerializeField] PlayerBehaviour _player;
    [SerializeField] TurretBehaviour _turret;
    [SerializeField] Flower _flower;
    [SerializeField] UpgradeManager _upgradeManager;
    [SerializeField] LevelManager _levelManager;

    static GlobalRefs _instance = null;

    static GlobalRefs _safeInstance 
    { 
        get
        {
            if (_instance == null)
                _instance = GameObject.FindGameObjectWithTag("GlobalRefs").GetComponent<GlobalRefs>();
            return _instance;
        }
    }
    public static PlayerBehaviour Player { get { return _safeInstance._player; } }
    public static TurretBehaviour Turret { get { return _safeInstance._turret; } }
    public static Flower Flower { get { return _safeInstance._flower; } }
    public static UpgradeManager UpgradeManager { get { return _safeInstance._upgradeManager; } }
    public static LevelManager LevelManger { get { return _safeInstance._levelManager; } }
    private void Awake()
    {
        _instance = this;
    }

}
