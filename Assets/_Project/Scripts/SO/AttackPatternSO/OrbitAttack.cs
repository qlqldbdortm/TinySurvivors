using TinySurvivors.ObjectPool;
using TinySurvivors.Projectile;
using TinySurvivors.Weapon;
using UnityEngine;

namespace TinySurvivors.SO.AttackPatternSO
{
    [CreateAssetMenu(fileName = "OrbitAttack", menuName = "ScriptableObjects/AttackPattern/02.OrbitAttack", order = 2)]
    public class OrbitAttack : AttackPatternData
    {
        public override void Execute(WeaponInstance weapon, Transform origin)
        {
            int count = weapon.ProjectileCount;
            float step = 2 * Mathf.PI / count;

            var prefab = weapon.Data.baseProjectilePrefab.GetComponent<BaseProjectile>();
            
            for (int i = 0; i < count; i++)
            {
                float angle = step * i;
                var proj = PoolManager.Instance.Spawn(prefab);
                proj.SetPrefab(prefab);
                
                if (proj is OrbitProjectile orbit)
                {
                    orbit.Setup(origin, weapon.AttackRange, weapon.ProjectileSpeed, weapon.Damage, angle);
                }
            }
        }
    }
}