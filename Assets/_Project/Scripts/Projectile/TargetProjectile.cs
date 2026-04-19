using System.Threading;
using Cysharp.Threading.Tasks;
using TinySurvivors.ObjectPool;
using TinySurvivors.Unit.Monster;
using UnityEngine;

namespace TinySurvivors.Projectile
{
    /// <summary>
    /// 가장 가까운 적을 향해 투사체를 발사하는 공격
    /// </summary>
    public class TargetProjectile : BaseProjectile
    {
        public float Damage { get; private set; }
        private float Speed { get; set; }
        private float LifeTime { get; set; }
        private Vector2 Direction { get; set; }
        private int PierceCount { get; set; }

        private CancellationTokenSource token;

        private void Update()
        {
            transform.Translate(Direction * (Speed * Time.deltaTime), Space.World);
        }

        public override void Init()
        {
            
        }

        public override void Release()
        {
            token?.Cancel();
            token?.Dispose();
            token = null;
        }

        public void Setup(float damage, float speed, Vector2 direction, float lifeTime, int pierceCount)
        {
            Damage = damage;
            Speed = speed;
            Direction = direction.normalized;
            LifeTime = lifeTime;
            PierceCount = pierceCount;
            
            token?.Cancel();
            token = new CancellationTokenSource();
            _ = DespawnAsync(token.Token);
        }
        
        public void SetDirection()
        {
            float angle = Mathf.Atan2(Direction.y, Direction.x) * Mathf.Rad2Deg + 90f;
            transform.rotation = Quaternion.Euler(0, 0, angle);
        }

        public void OnHit(Monster monster)
        {
            monster.UnitHp.TakeDamage(Damage);
            PierceCount--;

            if (PierceCount <= 0)
            {
                PoolManager.Instance.Despawn(Prefab, this);
            }
        }
        
        private async UniTask DespawnAsync(CancellationToken ct)
        {
            await UniTask.WaitForSeconds(LifeTime, cancellationToken: ct);
            PoolManager.Instance.Despawn(Prefab, this);
        }
    }
}