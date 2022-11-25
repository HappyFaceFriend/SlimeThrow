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
        }
    }
}

