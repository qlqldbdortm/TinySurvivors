using TinySurvivors.Enum;
using TinySurvivors.InGame;
using TinySurvivors.Interface;
using TinySurvivors.Unit.Monster.Boss;

namespace TinySurvivors.Unit.Monster.State
{
    public class MonsterDeathState : IMonsterState
    {
        private readonly MonsterStateMachine stateMachine;
        public MonsterState State { get; }

        public MonsterDeathState(MonsterStateMachine stateMachine, MonsterState state)
        {
            this.stateMachine = stateMachine;
            State = state;
        }

        public void Enter()
        {
            stateMachine.Monster.SpriteController.DarkSprite();
            stateMachine.Anim.SetState(State);
            stateMachine.Monster.MonsterController.SetRigidBodyOff();
            stateMachine.Monster.MonsterColliderHandler.SetColliderOff();

            if (stateMachine.Monster is BossMonster)
            {
                DropManager.Instance.BossDropExp(stateMachine.Monster.transform.position, 100, 5);   
            }
            else
            {
                DropManager.Instance.DropExp(stateMachine.Monster.transform.position);
            }
            DropManager.Instance.DropItem(stateMachine.Monster.transform.position);
            InGameManager.Instance.KillCount++;
        }

        public void Update()
        {
        }

        public void Exit()
        {
        }
    }
}