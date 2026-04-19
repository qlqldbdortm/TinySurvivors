using TinySurvivors.ObjectPool;
using TinySurvivors.Projectile;
using UnityEngine;

namespace TinySurvivors.Unit.Monster
{
    public class MonsterAttack : MonoBehaviour
    {
        [SerializeField] private Transform firePoint;
        private Monster BaseMonster { get; set; }
        private Transform Target { get; set; }
        
        [SerializeField] private BaseProjectile projectile;
        
        public void Init(Transform target, Monster monster)
        {
            Target = target;
            BaseMonster = monster;
        }

        public void Attack()
        {
            Vector2 dir = Target.position - firePoint.position;

            var proj = PoolManager.Instance.Spawn(projectile);
            proj.SetPrefab(projectile);
            
            proj.transform.position = firePoint.position;
            if (proj is MonsterProjectile mp)
            {
                mp.SetDirection(dir, BaseMonster);
            }
        }

        public void BossCircleAttack(int bulletCount)
        {
            float angleStep = 360f / bulletCount;

            for (int i = 0; i < bulletCount; i++)
            {
                float angle = angleStep * i;
                Vector2 dir = Quaternion.Euler(0, 0, angle) * Vector2.up;
                
                var proj = PoolManager.Instance.Spawn(projectile);
                proj.SetPrefab(projectile);

                proj.transform.position = firePoint.position;

                if (proj is MonsterProjectile mp)
                {
                    mp.SetBossDirection(dir, BaseMonster);
                }
            }
        }

        public void BossConeAttack(int bulletCount, float spreadAngle)
        {
            Vector2 forward = (Target.position - BaseMonster.transform.position).normalized;
            
            if (bulletCount <= 1)
            {
                var proj = PoolManager.Instance.Spawn(projectile);
                proj.SetPrefab(projectile);
                proj.transform.position = firePoint.transform.position;
                if (proj is MonsterProjectile mp)
                {
                    mp.SetBossDirection(forward, BaseMonster);
                }
                return;
            }

            float angleStep = spreadAngle / (bulletCount - 1);
            float startAngle = -spreadAngle / 2f;

            for (int i = 0; i < bulletCount; i++)
            {
                float angle = startAngle + angleStep * i;
                Vector2 dir = Quaternion.Euler(0, 0, angle) * forward;

                var proj = PoolManager.Instance.Spawn(projectile);
                proj.SetPrefab(projectile);
                proj.transform.position = firePoint.transform.position;
                if (proj is MonsterProjectile mp)
                {
                    mp.SetBossDirection(dir, BaseMonster);
                }
            }
        }
    }
}