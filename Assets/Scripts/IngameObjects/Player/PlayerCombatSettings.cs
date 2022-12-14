using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Settings/PlayerCombatSettings")]
public class PlayerCombatSettings : ScriptableObject
{
    [SerializeField] float _maxHp;
    [SerializeField] float _attackPower;
    [SerializeField] float _attackSpeed;
    [SerializeField] float _damageAsBullet;
    [SerializeField] float _getInTurretRange;
    [SerializeField] float _grabRange;
    [SerializeField] float _pushToTowerRange;
    [SerializeField] Sprite _slotIcon;
    [SerializeField] Color _color;
    public float MaxHp { get { return _maxHp; } }
    public float AttackPower { get { return _attackPower; } }
    public float AttackSpeed { get { return _attackSpeed; } }
    public float DamageAsBullet { get { return _damageAsBullet; } }
    public float GetInTurretRange { get { return _getInTurretRange; } }
    public float GrabRange { get { return _grabRange; } }
    public float PushToTowerRange { get { return _pushToTowerRange; } }
    public Sprite SlotIcon { get { return _slotIcon; } }
    public Color Color { get { return _color; } }
}
