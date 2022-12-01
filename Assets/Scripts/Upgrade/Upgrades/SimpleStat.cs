using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Upgrades
{
    [CreateAssetMenu(menuName = "Upgrade Datas/Simple Stat")]
    public class SimpleStat : UpgradeData
    {
        [System.Serializable]
        public class StatMode
        {
            public enum StatType { MaxHp, CurrentHP, AttackPower, AttackSpeed, MoveSpeed, PushToTowerRange }
            public StatType _statToChange;
            public float _applyValue;
            public Modifier.ApplyType _applyType;
        }
       
        [SerializeField] List<StatMode> _statModes;

        public override void OnAdded()
        {
            base.OnAdded();
            GenerateChange(_statModes);
        }

        public void GenerateChange(List<StatMode> modes)
        {
            for (int i = 0; i < modes.Count; i++)
            {
                var stat = modes[i];
                Modifier modifier = new Modifier(stat._applyValue, stat._applyType);
                if (stat._statToChange == StatMode.StatType.MaxHp)
                {
                    GlobalRefs.Player.MaxHp.AddModifier(modifier);
                    GlobalRefs.Player.PlayerHPBar.SetHp((int)GlobalRefs.Player.HpSystem.CurrentHp, (int)GlobalRefs.Player.HpSystem.MaxHp.Value);
                }
                    
                else if (stat._statToChange == StatMode.StatType.CurrentHP)
                {
                    GlobalRefs.Player.HpSystem.ChangeHp(stat._applyValue);
                    GlobalRefs.Player.PlayerHPBar.SetHp((int)GlobalRefs.Player.HpSystem.CurrentHp, (int)GlobalRefs.Player.HpSystem.MaxHp.Value);
                }
                    
                else if (stat._statToChange == StatMode.StatType.AttackPower)
                    GlobalRefs.Player.AttackPower.AddModifier(modifier);
                else if (stat._statToChange == StatMode.StatType.AttackSpeed)
                    GlobalRefs.Player.AttackSpeed.AddModifier(modifier);
                else if (stat._statToChange == StatMode.StatType.MoveSpeed)
                    GlobalRefs.Player.MoveSpeed.AddModifier(modifier);
                else if (stat._statToChange == StatMode.StatType.PushToTowerRange)
                    GlobalRefs.Player.PushToTowerRange.AddModifier(modifier);
            }
        }
        
    }
}