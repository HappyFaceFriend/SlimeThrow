using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Upgrades
{
    [CreateAssetMenu(menuName = "Upgrade Datas/Slime Pickup")]
    public class SlimePickup : UpgradeData
    {
        [SerializeField] SlimeData _grabbableSlime;
        [SerializeField] float _probability;
        
        public SlimeData Slime { get { return _grabbableSlime; } }
        public float Probability { get { return _probability; } }   

    }
}
