using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Upgrades
{
    [CreateAssetMenu(menuName = "Upgrade Datas/DebuffAllSlime")] 
    public class DebuffAllSlime : UpgradeData
    {
        [System.Serializable]
        public class DebuffModes
        {
            public enum DebuffType { Burning_Ground, Snowy_Field}
            public DebuffType _typeToChange;
        }
        [SerializeField] DebuffModes _debuffModes;

        public override void OnAdded()
        {
            base.OnAdded();
            SetDebuff(_debuffModes);
        }

        public void SetDebuff(DebuffModes mode)
        {
            if (mode._typeToChange == DebuffModes.DebuffType.Burning_Ground)
                GlobalRefs.LevelManger._spawner.BurningOn();
            else if (mode._typeToChange == DebuffModes.DebuffType.Snowy_Field)
                GlobalRefs.LevelManger._spawner.SnowyOn();
        }
    }
}

