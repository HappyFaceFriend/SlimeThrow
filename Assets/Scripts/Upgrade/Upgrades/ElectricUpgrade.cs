using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Upgrades
{
    [CreateAssetMenu(menuName = "Upgrade Datas/Electric Upgrades")]
    public class ElectricUpgrade : UpgradeData
    {
        [System.Serializable]
        public class UpgradeMode
        {
            public enum UpgradeType { Charge, Fry, Mega, Static, Nonconduct, Evolution }
            public UpgradeType _upgradeType;
        }

        [SerializeField] UpgradeMode _upgradeMode;

        public override void OnAdded()
        {
            base.OnAdded();
        }
    }
}

