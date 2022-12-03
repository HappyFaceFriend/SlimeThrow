using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Slime Data")]
public class SlimeData : ScriptableObject
{
    [SerializeField] float _maxHp;
    [SerializeField] float _attackPower;
    [SerializeField] float _flowerAttackPower = 3f;
    [SerializeField] float _attackSpeed;
    [SerializeField] float _attackRange;
    [SerializeField] float _sightRange;
    [SerializeField] float _moveSpeed;
    [SerializeField] float _damageAsBullet;
    [SerializeField] SlimeType _type;
    [SerializeField] Sprite _slotIcon;
    [SerializeField] Color _slimeColor = Color.white;

    public float MaxHp { get { return _maxHp; } }
    public float AttackPower { get { return _attackPower; } }
    public float FlowerAttackPower { get { return _flowerAttackPower; } }
    public float AttackSpeed { get { return _attackSpeed; } }
    public float AttackRange { get { return _attackRange; } }
    public float SightRange { get { return _sightRange; } }
    public float MoveSpeed { get { return _moveSpeed; } }
    public float DamageAsBullet { get { return _damageAsBullet; } }
    public SlimeType Type { get { return _type; } }
    public Sprite SlotIcon { get { return _slotIcon; } }
    public Color Color { get { return _slimeColor; } }
}

public enum SlimeType
{
    Bullet, Combat
}
