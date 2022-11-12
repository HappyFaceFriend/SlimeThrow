using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Upgrades
{
    [CreateAssetMenu(menuName = "Upgrade Datas/ContionalStat")]
    public class ContionalStat : UpgradeData
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
                if (stat._statToChange == StatMode.StatType.CurrentHP)
                    GlobalRefs.Player.SetUpgrade(stat._applyValue);
            }
        }
    }
}