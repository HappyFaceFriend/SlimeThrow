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
            public enum StatType { Hunter, Fierce }
            public StatType _statToChange;
            public float _applyValue;
        }

        [SerializeField] StatMode _statModes;

        public override void OnAdded()
        {
            base.OnAdded();
            GenerateChange(_statModes);
        }

        public void GenerateChange(StatMode modes)
        {
            var stat = modes;
            if (stat._statToChange == StatMode.StatType.Hunter)
                GlobalRefs.Player.SetHunter(stat._applyValue);
            else if (stat._statToChange == StatMode.StatType.Fierce)
                GlobalRefs.Player.SetFierce(stat._applyValue);
        }
    }
}