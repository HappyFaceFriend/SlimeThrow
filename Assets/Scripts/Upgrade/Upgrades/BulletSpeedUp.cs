using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Upgrades
{
    [CreateAssetMenu(menuName = "Upgrade Datas/Bullet SpeedUp")]
    public class BulletSpeedUp : UpgradeData
    {
        [SerializeField] float _applyValue;

        public override void OnAdded()
        {
            base.OnAdded();
            GlobalRefs.Turret._bulletBuilder.setValue(_applyValue);
        }
    }
}

