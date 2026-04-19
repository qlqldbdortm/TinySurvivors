using System;
using TinySurvivors.SO.WeaponSO;

namespace TinySurvivors.Weapon
{
    [Serializable]
    public class WeaponInstance
    {
        public WeaponData Data { get; private set; }
        public int Level { get; set; }
        public float Damage { get; set; }
        public float Cooldown { get; set; }
        public float Interval { get; set; }
        public float AttackRange { get; set; }
        public int ProjectileCount { get; set; }
        public float ProjectileSpeed { get; set; }
        public bool IsMaxLevel => Level >= Data.maxLevel;
        
        public WeaponInstance(WeaponData weaponData)
        {
            Data = weaponData;
            Level = 1;
            Damage = Data.baseWeaponDamage;
            CalculateAll();
        }

        public void WeaponLevelUp()
        {
            Level += 1;
            CalculateAll();
        }

        private void CalculateAll()
        {
            var lvData = Data.weaponLevelData[Level - 1];

            Damage = Data.baseWeaponDamage * lvData.damageMultiplier;
            Interval = Data.baseAttackInterval * lvData.intervalMultiplier;
            AttackRange = Data.baseAttackRange;
            ProjectileSpeed = Data.baseProjectileSpeed;
            ProjectileCount = Data.baseProjectileCount + lvData.additionalProjectile;
        }
    }
}