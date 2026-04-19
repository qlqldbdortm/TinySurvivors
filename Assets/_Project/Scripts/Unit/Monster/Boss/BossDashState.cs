using TinySurvivors.Enum;
using TinySurvivors.Interface;
using TinySurvivors.Unit.Monster.Boss;
using UnityEngine;

namespace TinySurvivors.Unit.Monster.State
{
    public class BossDashState : IMonsterState
    {
        private readonly MonsterStateMachine stateMachine;
        private readonly BossMonster boss;
        public MonsterState State { get; }
        
        private Vector2 startPos;
        private Vector2 dashDir;
        private float dashDistance = 10f;

        
        public BossDashState(MonsterStateMachine stateMachine, MonsterState state)
        {
            this.stateMachine = stateMachine;
            State = state;
            boss = stateMachine.Monster as BossMonster;
        }
        public void Enter()
        {
            startPos = boss.transform.position;
            dashDir = (boss.Target.position - boss.transform.position).normalized;
            
            boss.MonsterController.StartDash(dashDir, 6);
        }

        public void Update()
        {
            float moved = Vector2.Distance(startPos, boss.transform.position);

            if (moved >= dashDistance)
            {
                boss.MonsterController.EndDash();
                stateMachine.ChangeState(MonsterState.BossChase);
            }
        }

        public void Exit()
        {
            boss.MonsterController.EndDash();
        }
    }
}