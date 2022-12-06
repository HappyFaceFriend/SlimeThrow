using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Upgrades
{
    [CreateAssetMenu(menuName = "Upgrade Datas/Ice Upgrades")]
    public class IceUpgrade : UpgradeData
    {
        [System.Serializable]
        public class UpgradeMode
        {
            public enum UpgradeType { SnowIce, Freeze, Cold, Wave, IceCoal, Freezer}
            public UpgradeType _upgradeType;
        }

        [SerializeField] UpgradeMode _upgradeMode;

        public override void OnAdded()
        {
            base.OnAdded();
        }
    }
}


