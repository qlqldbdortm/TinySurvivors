using System.Linq;
using System.Threading;
using Cysharp.Threading.Tasks;
using TinySurvivors.ObjectPool;
using TinySurvivors.Projectile.Hitbox;
using UnityEngine;

namespace TinySurvivors.Projectile
{
    /// <summary>
    /// 플레이어 주변 일정 범위를 랜덤으로 공격하는 공격 (지속형)
    /// </summary>
    public class DotAreaProjectile : BaseProjectile
    {
        [SerializeField] private ParticleSystem particle;
        public float Damage { get; private set; }
        public float AttackDelay { get; private set;}
        private float LifeTime { get; set; }
        
        private float lifeTimer;
        private float attackTimer;
        private bool isActive;
        private bool isDespawned;
        
        private AreaHitbox areaHitbox;
        public override void Init()
        {
            areaHitbox = GetComponentInChildren<AreaHitbox>();
            isActive = false;
        }

        public override void Release()
        {
            isActive = false;
            isDespawned = false;

            particle?.Stop(true, ParticleSystemStopBehavior.StopEmittingAndClear);
        }

        public void Setup(float damage, float lifeTime, float attackDelay)
        {
            Damage = damage;
            LifeTime = lifeTime;
            AttackDelay = attackDelay;

            lifeTimer = 0f;
            attackTimer = 0f;
            isActive = true;

            particle.Play(true);
        }

        private void Update()
        {
            if (!isActive) return;

            lifeTimer += Time.deltaTime;
            attackTimer += Time.deltaTime;

            // 공격
            if (attackTimer >= AttackDelay)
            {
                attackTimer -= AttackDelay;

                var targets = areaHitbox.AreaTargets;
                foreach (var monster in targets)
                {
                    monster?.UnitHp.TakeDamage(Damage);
                }
            }

            // 수명 종료
            if (lifeTimer >= LifeTime)
            {
                SafeDespawn();
            }
        }

        private void SafeDespawn()
        {
            if (isDespawned) return;
            isDespawned = true;

            isActive = false;
            PoolManager.Instance.Despawn(Prefab, this);
        }
    }
}