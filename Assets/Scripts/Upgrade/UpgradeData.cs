using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[CreateAssetMenu(menuName = "Upgrade Data")]
public class UpgradeData : ScriptableObject
{
    [SerializeField] string _name;
    [SerializeField] string _body;
    [SerializeField] Sprite _icon;

    public string Name { get { return _name; } }
    public string Body { get { return _body; } }
    public Sprite Icon{ get { return _icon; } }
}
