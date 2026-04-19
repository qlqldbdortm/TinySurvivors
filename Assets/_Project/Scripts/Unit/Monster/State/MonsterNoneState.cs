
using TinySurvivors.Enum;
using TinySurvivors.Interface;

namespace TinySurvivors.Unit.Monster.State
{
    public class MonsterNoneState : IMonsterState
    {
        private readonly MonsterStateMachine stateMachine;
        public MonsterState State { get; }

        /// <summary>
        /// 몬스터가 Pool에 들어가있을때 넘어오는 State. 아무기능도 하지 않음
        /// </summary>
        public MonsterNoneState(MonsterStateMachine stateMachine, MonsterState state)
        {
            this.stateMachine = stateMachine;
            State = state;
        }
        public void Enter()
        {
            if (!stateMachine.gameObject.activeInHierarchy) return;
            // Monster가 Pool에 들어가게 되면 애니메이션과 애니메이터를 초기화 하고
            stateMachine.Anim.Animator.Rebind();
            stateMachine.Anim.Animator.Update(0f);
            
            // 공격단계에서 Monster가 Pool에 들어가게 되면 IsAttacking이 true가 된상태로 들어가
            // 이후 공격에 문제가 생기므로 false로 변경
            stateMachine.Monster.MonsterController.IsAttacking = false;
        }

        public void Update()
        {
            
        }

        public void Exit()
        {
            
        }
    }
}