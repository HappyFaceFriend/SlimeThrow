using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum Rarity
{
    Low, Mid, High
}

[CreateAssetMenu(menuName = "Upgrade Data")]
public class UpgradeData : ScriptableObject
{
    [SerializeField] string _name;
    [SerializeField] string _body;
    [SerializeField] Sprite _icon;
    [SerializeField] Rarity _rarity;
    [SerializeField] int _maxCount = 1;
    public string Name { get { return _name; } }
    public string Body { get { return _body; } }
    public Sprite Icon{ get { return _icon; } }

    public Rarity Rarity { get { return _rarity; } }

    public int MaxCount { get { return _maxCount; } }
    public virtual void OnAdded()
    {

    }
    public virtual void OnUpdate()
    {

    }
}
