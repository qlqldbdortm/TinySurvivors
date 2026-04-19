using TinySurvivors.InGame;
using TinySurvivors.ObjectPool;
using TinySurvivors.Projectile;
using TinySurvivors.Weapon;
using UnityEngine;

namespace TinySurvivors.SO.AttackPatternSO
{ 
    [CreateAssetMenu(fileName = "MeleeAttack", menuName = "ScriptableObjects/AttackPattern/01.MeleeAttack", order = 1)]
    public class MeleeAttack : AttackPatternData
    {
        public override void Execute(WeaponInstance weapon, Transform origin)
        {
            var prefab = weapon.Data.baseProjectilePrefab.GetComponent<BaseProjectile>();
            var proj = PoolManager.Instance.Spawn(prefab);
            var prevParent = proj.transform.parent;

            proj.SetPrefab(prefab);
            proj.transform.SetParent(origin);
            proj.transform.localPosition = Vector3.zero;
            proj.transform.rotation = origin.rotation;

            if (proj is MeleeProjectile melee)
            {
                Vector2 dir = GetAttackDirection(origin);
                
                var baseAngle = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg;
                melee.Setup(weapon.Damage, 0.2f, 45, baseAngle, prevParent);
            }
        }

        private Vector2 GetAttackDirection(Transform origin)
        {
            var closestMonster = MonsterManager.Instance.FindClosest(origin, float.MaxValue);
            return closestMonster is not null ? (closestMonster.transform.position - origin.position).normalized : origin.right;
        }
    }
}