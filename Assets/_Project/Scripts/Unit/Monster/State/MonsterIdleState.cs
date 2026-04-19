using TinySurvivors.Enum;
using TinySurvivors.Interface;

namespace TinySurvivors.Unit.Monster.State
{
    public class MonsterIdleState : IMonsterState
    {
        private readonly MonsterStateMachine stateMachine;
        public MonsterState State { get; }

        public MonsterIdleState(MonsterStateMachine stateMachine, MonsterState state)
        {
            this.stateMachine = stateMachine;
            State = state;
        }
        public void Enter()
        {
            stateMachine.Monster.SpriteController.ResetSprite();
            stateMachine.Anim.SetState(State);
            stateMachine.Monster.MonsterController.SetRigidBodyOn();
            stateMachine.Monster.MonsterColliderHandler.SetColliderOn();
        }

        public void Update()
        {
            // 몬스터는 테어난 직후 바로 플레이어를 추적해서 이동해야 하므로 Chase로 넘어감.
            stateMachine.ChangeState(MonsterState.Chase);
        }

        public void Exit()
        {
            
        }
    }
}