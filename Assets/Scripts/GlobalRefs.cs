using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GlobalRefs : MonoBehaviour
{
    [SerializeField] PlayerBehaviour _player;
    [SerializeField] TurretBehaviour _turret;
    [SerializeField] Flower _flower;

    static GlobalRefs _instance = null;

    public static PlayerBehaviour Player { get { return _instance._player; } }
    public static TurretBehaviour Turret { get { return _instance._turret; } }
    public static Flower Flower { get { return _instance._flower; } }
    private void Awake()
    {
        _instance = this;
    }

}
