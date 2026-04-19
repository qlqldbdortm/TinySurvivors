using System.Threading;
using Cysharp.Threading.Tasks;
using TinySurvivors.ObjectPool;
using TinySurvivors.Unit.Monster;

namespace TinySurvivors.Projectile
{
    /// <summary>
    /// 플레이어 주변 일정 범위를 랜덤으로 공격하는 공격 (단발형)
    /// </summary>
    public class PeriodicAreaProjectile : BaseProjectile
    {
        public float Damage { get; private set; }
        private float LifeTime { get; set; }

        private int spawnId;
        
        public override void Init()
        {
            spawnId++;
        }

        public override void Release()
        {
        }

        public void Setup(float damage, float lifeTime)
        {
            Damage = damage;
            LifeTime = lifeTime;
            
            _ = DespawnAsync(spawnId);
        }

        private async UniTask DespawnAsync(int id)
        {
            await UniTask.WaitForSeconds(LifeTime);

            if (id != spawnId) return;

            PoolManager.Instance.Despawn(Prefab, this);
        }

        public void OnHit(Monster monster)
        {
            monster.UnitHp.TakeDamage(Damage);
        }
    }
}