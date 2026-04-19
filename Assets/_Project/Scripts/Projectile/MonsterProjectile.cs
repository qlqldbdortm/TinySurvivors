using System.Threading;
using Cysharp.Threading.Tasks;
using TinySurvivors.ObjectPool;
using TinySurvivors.Unit.Monster;
using TinySurvivors.Unit.Player;
using UnityEngine;

namespace TinySurvivors.Projectile
{
    /// <summary>
    /// 몬스터가 플레이어를 향해서 투사체를 발사하는 공격
    /// </summary>
    public class MonsterProjectile : BaseProjectile
    {
        [SerializeField] private float speed = 10f;
        [SerializeField] private float lifeTime = 5f;
        [SerializeField] private ProjRotate rotate;
        private Vector2 direction;
        private int spawnId;
        public Monster Shooter { get; private set; }

        public override void Init()
        {
            spawnId++;
            rotate?.StartRotate();
        }

        public override void Release()
        {
            direction = Vector2.zero;

            rotate?.StopRotate();
        }

        private void Update()
        {
            transform.position += (Vector3)(direction * (speed * Time.deltaTime));
        }


        public void OnHit(PlayerColliderHandler player)
        {
            player.BasePlayer.UnitHp.TakeDamage(Shooter.MonsterStat.Damage.Value);
            PoolManager.Instance.Despawn(Prefab, this);
        }
        
        #region ◆ 일반 몬스터 공격 ◆

        public void SetDirection(Vector2 dir, Monster monster)
        {
            Shooter = monster;
            direction = dir.normalized;

            float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg + 90f;
            transform.rotation = Quaternion.Euler(0, 0, angle);
            
            _ = DespawnAsync(spawnId);
        }

        #endregion
        #region ◆ 보스 몬스터 공격 ◆

        public void SetBossDirection(Vector2 dir, Monster monster)
        {
            Shooter = monster;
            direction = dir.normalized;

            float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg + 90f;
            transform.rotation = Quaternion.Euler(0, 0, angle);
            
            _ = DespawnAsync(spawnId);
        }

        #endregion
        
        private async UniTask DespawnAsync(int id)
        {
            await UniTask.WaitForSeconds(lifeTime);

            if (id != spawnId) return;

            PoolManager.Instance.Despawn(Prefab, this);
        }
    }
}