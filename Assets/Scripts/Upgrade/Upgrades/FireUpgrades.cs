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
            public enum UpgradeType { Burning, Flames, Critical_Fire, Fire_Cannon, Fire_Slayer, Flamemail, Flame_Bullet, Burning_Fist }
            public UpgradeType _upgradeType;
            public float _applyValue;
            public Modifier.ApplyType _applyType;
        }

        [SerializeField] UpgradeMode _upgradeMode;

        public override void OnAdded()
        {
            base.OnAdded();
        }
    }
}