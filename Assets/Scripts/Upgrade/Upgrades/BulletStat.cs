using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Upgrades
{
    [CreateAssetMenu(menuName = "Upgrade Datas/Bullet Stat")]
    public class BulletStat : UpgradeData
    {
        [System.Serializable]
        public class StatMode
        {
            public enum Stattype { SpeedUp, DamageUp }
            public Stattype _statToChange;
            public float _applyValue;
            public Modifier.ApplyType _applyType;
        }

        [SerializeField] StatMode _statMode;
        
        public override void OnAdded()
        {
            base.OnAdded();
            Generate(_statMode);
        }

        public void Generate(StatMode mode)
        {
            var stat = mode;
            Modifier modifier = new Modifier(stat._applyValue, stat._applyType);
            if (mode._statToChange == StatMode.Stattype.SpeedUp)
                GlobalRefs.Turret._bulletBuilder.Speed.AddModifier(modifier);
            else if (mode._statToChange == StatMode.Stattype.DamageUp)
                GlobalRefs.LevelManger.Spawner.ExtraDamage.AddModifier(modifier);
        }
    }
}

