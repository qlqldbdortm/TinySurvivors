using TinySurvivors.ObjectPool;
using TinySurvivors.Projectile;
using TinySurvivors.Weapon;
using UnityEngine;

namespace TinySurvivors.SO.AttackPatternSO
{
    [CreateAssetMenu(fileName = "AreaAttack", menuName = "ScriptableObjects/AttackPattern/05.AreaAttack", order = 5)]
    public class AreaAttack : AttackPatternData
    {
        public float lifeTime;
        public float attackDelay;
        public float minRadius;
        public float maxRadius;
        
        public override void Execute(WeaponInstance weapon, Transform origin)
        {
            var prefab = weapon.Data.baseProjectilePrefab.GetComponent<BaseProjectile>();

            for (int i = 0; i < weapon.ProjectileCount; i++)
            {
                var proj = PoolManager.Instance.Spawn(prefab);
                proj.SetPrefab(prefab);

                Vector2 offset = Random.insideUnitCircle * Random.Range(minRadius, maxRadius);

                proj.transform.position = origin.position + (Vector3)offset;

                if (proj is DotAreaProjectile area)
                {
                    area.Setup(weapon.Damage, lifeTime, attackDelay);
                }
                if (proj is PeriodicAreaProjectile periodic)
                {
                    periodic.Setup(weapon.Damage, lifeTime);
                }
            }
        }
    }
}