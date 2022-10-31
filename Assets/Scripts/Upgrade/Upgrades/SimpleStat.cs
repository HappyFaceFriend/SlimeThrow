using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Upgrades
{
    [CreateAssetMenu(menuName = "Upgrade Datas/Simple Stat")]
    public class SimpleStat : UpgradeData
    {
        public enum StatType { MaxHp, AttackPower, AttackSpeed, MoveSpeed, PushToTowerRange }

        [SerializeField] int _numberOfStats;
        [SerializeField] StatType _statToChange;
        [SerializeField] float _applyValue;
        [SerializeField] Modifier.ApplyType _applyType;
        [SerializeField] 

        public override void OnAdded()
        {
            base.OnAdded();
            Modifier modifier = new Modifier(_applyValue, _applyType);
            if (_statToChange == StatType.MaxHp)
                GlobalRefs.Player.MaxHp.AddModifier(modifier);
            else if (_statToChange == StatType.AttackPower)
                GlobalRefs.Player.AttackPower.AddModifier(modifier);
            else if (_statToChange == StatType.AttackSpeed)
                GlobalRefs.Player.AttackSpeed.AddModifier(modifier);
            else if (_statToChange == StatType.MoveSpeed)
                GlobalRefs.Player.MoveSpeed.AddModifier(modifier);
            else if (_statToChange == StatType.PushToTowerRange)
                GlobalRefs.Player.PushToTowerRange.AddModifier(modifier);
        }
    }
}