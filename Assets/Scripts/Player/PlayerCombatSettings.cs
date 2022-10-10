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
    [SerializeField] Sprite _slotIcon;

    public float MaxHp { get { return _maxHp; } }
    public float AttackPower { get { return _attackPower; } }
    public float AttackSpeed { get { return _attackSpeed; } }
    public float DamageAsBullet { get { return _damageAsBullet; } }
    public Sprite SlotIcon { get { return _slotIcon; } }
}
