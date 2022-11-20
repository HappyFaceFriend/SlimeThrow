using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Upgrades
{
    
    [CreateAssetMenu(menuName = "Upgrade Datas/Fire Upgrades")]
    public class FireUpgrades : UpgradeData
    {
        [System.Serializable]
        public class UpgradeMode
        {
            public enum UpgradeType { Burning, Flames, Critical_Fire, Fire_Cannon, Fire_Slayer }
            public UpgradeType _upgradeType;
            public float _applyValue;
            public Modifier.ApplyType _applyType;
        }

        [SerializeField] UpgradeMode _upgradeMode;

        public override void OnAdded()
        {
            base.OnAdded();
            GenerateUpgrade(_upgradeMode);
        }

        public void GenerateUpgrade(UpgradeMode mode)
        {
            Modifier modifier = new Modifier(mode._applyValue, mode._applyType);
            UpgradeMode.UpgradeType type = mode._upgradeType;
            if (type == UpgradeMode.UpgradeType.Burning)
                GlobalRefs.EffectStatManager._burn.DamagePerTick.AddModifier(modifier);
            else if (type == UpgradeMode.UpgradeType.Flames)
                GlobalRefs.EffectStatManager._burn.Duration.AddModifier(modifier);
            else if (type == UpgradeMode.UpgradeType.Critical_Fire)
                GlobalRefs.LevelManger._spawner._criticalUpgrade = true;
            else if (type == UpgradeMode.UpgradeType.Fire_Cannon)
                GlobalRefs.LevelManger._spawner._fireBallUpgrade = true;
            else if (type == UpgradeMode.UpgradeType.Fire_Slayer)
                GlobalRefs.LevelManger._spawner._fireSlayerUpgrade = true;
        }
    }
}


