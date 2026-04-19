using TinySurvivors.Enum;
using TinySurvivors.Interface;
using TinySurvivors.Unit.Monster.State;
using UnityEngine;

namespace TinySurvivors.Unit.Monster.Boss
{
    public class BossBulletState : IMonsterState
    {
        private readonly MonsterStateMachine stateMachine;
        private readonly BossMonster boss;
        public MonsterState State { get; }

        public BossBulletState(MonsterStateMachine stateMachine, MonsterState state)
        {
            this.stateMachine = stateMachine;
            State = state;
            boss = stateMachine.Monster as BossMonster;
        }
        
        public void Enter()
        {
            boss.SetBulletPattern((BossBulletPattern)Random.Range(1, 3));
            stateMachine.Anim.SetState(State);
        }

        public void Update()
        {
            if (boss.MonsterController.IsAttacking) return;
            stateMachine.ChangeState(MonsterState.BossChase);
        }

        public void Exit()
        {
            
        }
    }
}