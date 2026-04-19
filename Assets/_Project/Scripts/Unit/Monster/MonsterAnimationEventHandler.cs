using System;
using TinySurvivors.Enum;
using TinySurvivors.ObjectPool;
using TinySurvivors.Unit.Monster.Boss;

namespace TinySurvivors.Unit.Monster
{
    public class MonsterAnimationEventHandler : UnitAnimationEventHandler
    {
        public event Action OnBossDeath;
        private Monster Monster { get; set; }
        
        public void Init(Monster monster)
        {
            Monster = monster;
        }


        public override void OnAttackStart()
        {
            Monster.MonsterController.IsAttacking = true;
        }

        public override void OnShootProj()
        {
            switch (Monster)
            {
                case BossMonster boss:
                    FireBossPattern(boss);
                    break;

                default:
                    Monster.MonsterAttack.Attack();
                    break;
            }
        }

        public override void OnAttackEnd()
        {
            Monster.MonsterController.IsAttacking = false;
        }

        public override void OnDeath()
        {
            if (Monster.Prefab is not null)
            {
                PoolManager.Instance.Despawn(Monster.Prefab, Monster);
                OnBossDeath?.Invoke();
            }
        }
        
        private void FireBossPattern(BossMonster boss)
        {
            switch (boss.CurrentBulletPattern)
            {
                case BossBulletPattern.Circle:
                    boss.MonsterAttack.BossCircleAttack(12);
                    break;

                case BossBulletPattern.Cone:
                    boss.MonsterAttack.BossConeAttack(7, 60f);
                    break;
            }
        }
    }
}