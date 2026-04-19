using TinySurvivors.Enum;
using TinySurvivors.Interface;
using TinySurvivors.Unit.Monster.Boss;
using TinySurvivors.Unit.Monster.State;
using UnityEngine;

namespace _Project.Scripts.Unit.Monster.Boss
{
    public class BossChaseState : IMonsterState
    {
        private readonly MonsterStateMachine stateMachine;
        private readonly BossMonster boss;
        private readonly float chaseDuration = 5f;
        public MonsterState State { get; }

        private float chaseTimer;

        public BossChaseState(MonsterStateMachine stateMachine, MonsterState state)
        {
            this.stateMachine = stateMachine;
            State = state;
            boss = stateMachine.Monster as BossMonster;
        }

        public void Enter()
        {
            stateMachine.Anim.SetState(MonsterState.Chase);
            chaseTimer = 0f;
        }

        public void Update()
        {
            chaseTimer += Time.deltaTime;
            
            // 보스가 패턴을 실행하고 일정 시간동안 플레이어를 추격하닥 다시 패턴을 사용하도록
            if (chaseTimer >= chaseDuration)
            {
                stateMachine.ChangeState(MonsterState.BossPattern);
            }
            
            boss.MonsterController.MoveTo(boss.Target.position);
            boss.transform.localScale = boss.MonsterController.IsRight
                ? new Vector3(-1, 1, 1) * boss.transform.localScale.z
                : new Vector3(1, 1, 1) * boss.transform.localScale.z;
        }

        public void Exit()
        {
        }
    }
}