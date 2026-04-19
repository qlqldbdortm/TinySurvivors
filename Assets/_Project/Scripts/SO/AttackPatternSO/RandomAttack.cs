using TinySurvivors.ObjectPool;
using TinySurvivors.Projectile;
using TinySurvivors.Weapon;
using UnityEngine;

namespace TinySurvivors.SO.AttackPatternSO
{
    [CreateAssetMenu(fileName = "RandomAttack", menuName = "ScriptableObjects/AttackPattern/03.RandomAttack", order = 3)]
    public class RandomAttack : AttackPatternData
    {
        public float lifeTime = 3f;
        
        public override void Execute(WeaponInstance weapon, Transform origin)
        {
            // TODO : 랜덤한 방향으로 투사체를 사출하는 공격 구현
            int count = weapon.ProjectileCount;
            var prefab = weapon.Data.baseProjectilePrefab.GetComponent<BaseProjectile>();
            
            for (int i = 0; i < count; i++)
            {   
                var proj = PoolManager.Instance.Spawn(prefab);
                proj.SetPrefab(prefab);
                proj.transform.position = origin.position;
                
                if (proj is RandomProjectile random)
                {
                    random.Setup(weapon.Damage, weapon.ProjectileSpeed, lifeTime);
                    random.SetDirection();
                }
            }
        }
    }
}