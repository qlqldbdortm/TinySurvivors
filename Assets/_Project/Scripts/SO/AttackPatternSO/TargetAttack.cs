using TinySurvivors.InGame;
using TinySurvivors.ObjectPool;
using TinySurvivors.Projectile;
using TinySurvivors.Weapon;
using UnityEngine;

namespace TinySurvivors.SO.AttackPatternSO
{
    [CreateAssetMenu(fileName = "TargetAttack", menuName = "ScriptableObjects/AttackPattern/04.TargetAttack", order = 4)]
    public class TargetAttack : AttackPatternData
    {
        public float lifeTime = 3f;
        public int pierceCount = 1;
        
        public override void Execute(WeaponInstance weapon, Transform origin)
        {
            var prefab = weapon.Data.baseProjectilePrefab.GetComponent<BaseProjectile>();
            
            for (int i = 0; i < weapon.ProjectileCount; i++)
            {
                var target = MonsterManager.Instance.FindClosest(origin, weapon.AttackRange);
                if (target is null) return;
                Vector2 direction = (target.position - origin.position).normalized;
                
                var proj = PoolManager.Instance.Spawn(prefab);
            
                proj.SetPrefab(prefab);
                proj.transform.position = origin.position;
                
                if (proj is TargetProjectile range)
                {
                    range.Setup(weapon.Damage, weapon.ProjectileSpeed, direction, lifeTime, pierceCount);
                    range.SetDirection();
                }
            }
        }
    }
}