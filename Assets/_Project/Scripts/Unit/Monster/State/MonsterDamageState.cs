using TinySurvivors.Enum;
using TinySurvivors.Interface;
using UnityEngine;

namespace TinySurvivors.Unit.Monster.State
{
    public class MonsterDamageState : IMonsterState
    {
        private readonly MonsterStateMachine stateMachine;
        private readonly Monster monster;
        public MonsterState State { get; }
        
        private float time;
        private readonly float knockBackPower = 1.5f;
        private readonly float blinkTime = 0.5f;
        public MonsterDamageState(MonsterStateMachine stateMachine, MonsterState state)
        {
            this.stateMachine = stateMachine;
            State = state;
            monster = stateMachine.Monster;
        }
        public void Enter()
        {
            stateMachine.Monster.SpriteController.StartBlink();
            stateMachine.Monster.MonsterController.KnockBack(stateMachine.Monster.Target.position, knockBackPower);
        }

        public void Update()
        {
            time += Time.deltaTime;

            if (time > blinkTime)
            {
                stateMachine.Monster.SpriteController.StopBlink();
                time = 0;
                stateMachine.ChangeState(MonsterState.Chase);
            }
        }

        public void Exit()
        {
            stateMachine.Monster.SpriteController.StopBlink();
        }
    }
}