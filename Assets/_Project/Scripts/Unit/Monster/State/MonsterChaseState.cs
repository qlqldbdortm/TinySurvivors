using TinySurvivors.Enum;
using TinySurvivors.Interface;
using TinySurvivors.Unit.Monster.Boss;
using UnityEngine;

namespace TinySurvivors.Unit.Monster.State
{
    public class MonsterChaseState : IMonsterState
    {
        private readonly MonsterStateMachine stateMachine;
        public MonsterState State { get; }

        public MonsterChaseState(MonsterStateMachine stateMachine, MonsterState state)
        {
            this.stateMachine = stateMachine;
            State = state;
        }

        public void Enter()
        {
            stateMachine.Anim.SetState(State);
        }

        public void Update()
        {
            // 기본적으로 몬스터의 공격타입이 근거리면 공격 애니메이션을 따로 넣지 않고 몸통박치기로 피격
            // 원거리의 경우 활쏘는 모션 혹은 마법을 쏘는 모션이 나오도록.
            if (CanAttack())
            {
                stateMachine.ChangeState(MonsterState.Attack);
                return;
            }
            
            stateMachine.Monster.MonsterController.MoveTo(stateMachine.Monster.Target.position);
            stateMachine.Monster.transform.localScale = stateMachine.Monster.MonsterController.IsRight
                ? new Vector3(-1, 1, 1)
                : new Vector3(1, 1, 1);
        }

        public void Exit()
        {
        }

        private bool CanAttack()
        {
            var monster = stateMachine.Monster;
            return monster.DistanceToTarget() <= monster.MonsterStat.DetectRange.Value &&
                   (monster.MonsterStat.AttackType == AttackType.Range || monster is BossMonster);
        }
    }
}