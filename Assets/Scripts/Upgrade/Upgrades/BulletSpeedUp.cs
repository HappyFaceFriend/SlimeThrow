using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Upgrades
{
    [CreateAssetMenu(menuName = "Upgrade Datas/Bullet SpeedUp")]
    public class BulletSpeedUp : UpgradeData
    {
        [SerializeField] float _applyValue;
        [SerializeField] Modifier.ApplyType _applyType;

        public override void OnAdded()
        {
            base.OnAdded();
            Modifier modifier = new Modifier(_applyValue, _applyType);
            GlobalRefs.Turret._bulletBuilder.upgrade1 = true;
        }
    }
}

