using TinySurvivors.Enum;
using TinySurvivors.Interface;
using TinySurvivors.Unit.Monster.State;
using UnityEngine;

namespace TinySurvivors.Unit.Monster.Boss
{
    public class BossPatternState : IMonsterState
    {
        private readonly MonsterStateMachine stateMachine;
        private readonly BossMonster boss;
        public MonsterState State { get; }

        public BossPatternState(MonsterStateMachine stateMachine, MonsterState state)
        {
            this.stateMachine = stateMachine;
            State = state;
            boss = stateMachine.Monster as BossMonster;
        }
        public void Enter()
        {
            boss.patternTimer = 0f;
        }

        public void Update()
        {
            boss.patternTimer += Time.deltaTime;

            if (boss.patternTimer < boss.patternCooldown[boss.currentPattern])
                return;

            boss.patternTimer = 0f;

            switch (boss.currentPattern)
            {
                case 0:
                    stateMachine.ChangeState(MonsterState.BossDash);
                    break;
                case 1:
                    stateMachine.ChangeState(MonsterState.BossBullet);
                    break;
            }

            boss.currentPattern =
                (boss.currentPattern + 1) % boss.patternCooldown.Length;
        }

        public void Exit()
        {
        }
    }
}