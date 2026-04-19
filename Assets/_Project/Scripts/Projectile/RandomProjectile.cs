using System.Threading;
using Cysharp.Threading.Tasks;
using TinySurvivors.ObjectPool;
using TinySurvivors.Unit.Monster;
using UnityEngine;


namespace TinySurvivors.Projectile
{
    /// <summary>
    /// 플레이어를 중심으로 랜덤한 방향으로 여러개의 투사체를 발사하는 공격
    /// </summary>
    public class RandomProjectile : BaseProjectile
    {
        [SerializeField] private ParticleSystem particle;
        public float Damage { get; private set; }
        private float LifeTime { get; set; }
        private float Speed { get; set; }
        private Vector2 Direction { get; set; }
        
        private int spawnId;

        private void Update()
        {
            transform.Translate(Direction * (Speed * Time.deltaTime), Space.World);
        }

        public override void Init()
        {
            spawnId++;
            particle?.Stop(true, ParticleSystemStopBehavior.StopEmittingAndClear);
        }

        public override void Release()
        {
            Direction = Vector2.zero;
        }
        
        public void Setup(float damage, float speed, float lifeTime)
        {
            Damage = damage;
            Speed = speed;
            LifeTime = lifeTime;

            particle.Play(true);
            
            _ = DespawnAsync(spawnId);
        }

        public void SetDirection()
        {
            float angle = Random.Range(0, 360);
            Direction = Quaternion.Euler(0, 0, angle) * Vector2.right;
            
            float transformAngle = Mathf.Atan2(Direction.y, Direction.x) * Mathf.Rad2Deg + 90f;
            transform.rotation = Quaternion.Euler(0, 0, transformAngle);
            
        }

        public void OnHit(Monster monster)
        {
            monster.UnitHp.TakeDamage(Damage);
            PoolManager.Instance.Despawn(Prefab, this);
        }
        private async UniTask DespawnAsync(int id)
        {
            await UniTask.WaitForSeconds(LifeTime);

            if (id != spawnId) return;

            PoolManager.Instance.Despawn(Prefab, this);
        }
    }
}