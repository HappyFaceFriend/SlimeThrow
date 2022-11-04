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
        [SerializeField] List<DebuffModes> _debuffModes;

        public override void OnAdded()
        {
            base.OnAdded();
            Debuff(_debuffModes);
        }

        public void Debuff(List<DebuffModes> modes)
        {
            for(int i=0;i <modes.Count; i++)
            {
                var debuff = modes[i];
                if (debuff._typeToChange == DebuffModes.DebuffType.Burning_Ground)
                {
                    while (Time.deltaTime < 10)
                        GlobalRefs.LevelManger._spawner._burnUpgrade = true;
                    GlobalRefs.LevelManger._spawner._burnUpgrade = false;
                }
            }
        }
    }
}

