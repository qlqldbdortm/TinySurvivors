using System.Collections;
using System.Threading;
using Cysharp.Threading.Tasks;
using TinySurvivors.Enum;
using TinySurvivors.Interface;
using TinySurvivors.Unit.Monster.Boss;
using UnityEngine;

namespace TinySurvivors.Unit.Monster.State
{
    public class MonsterAttackState : IMonsterState
    {
        private readonly MonsterStateMachine stateMachine;
        private readonly Monster Monster;
        public MonsterState State { get; }
        
        public MonsterAttackState(MonsterStateMachine stateMachine, MonsterState state)
        {
            this.stateMachine = stateMachine;
            State = state;
            Monster = stateMachine.Monster;
        }
        
        public void Enter()
        {
            if (Monster is BossMonster) return;
            stateMachine.Anim.SetState(State);
        }
        
        public void Update()
        {
            // 공격 애니메이션 출력 중에는 상태가 변경되지 않도록 
            if (Monster.MonsterController.IsAttacking) return;
            
            if (Monster.IsBoss)
            {
                stateMachine.ChangeState(MonsterState.BossPattern);
                return;
            }
            
            // 몬스터의 공격 범위안에 플레이어가 있는경우 다시 공격 상태로 변경
            // 몬스터의 공격 범위에서 벗어난 경우 추격 상태로 변경
            stateMachine.ChangeState(CanAttack() ? MonsterState.Attack : MonsterState.Chase);
        }
        
        public void Exit()
        {
        }
        
        /// <summary>
        /// 플레이어가 공격 범위 안에 들어와 있는지 체크하는 메서드
        /// </summary>
        private bool CanAttack()
        {
            var monster = stateMachine.Monster;
            return monster.DistanceToTarget() <= monster.MonsterStat.DetectRange.Value &&
                   monster.MonsterStat.AttackType == AttackType.Range;
        }

    }
}