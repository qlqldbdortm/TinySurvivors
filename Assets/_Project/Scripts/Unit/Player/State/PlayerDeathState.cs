using TinySurvivors.Enum;
using TinySurvivors.InGame;
using TinySurvivors.Interface;

namespace TinySurvivors.Unit.Player.State
{
    public class PlayerDeathState : IPlayerState
    {
        private readonly PlayerStateMachine stateMachine;
        public PlayerState State { get; }

        public PlayerDeathState(PlayerStateMachine stateMachine, PlayerState state)
        {
            this.stateMachine = stateMachine;
            State = state;
        }
        
        public void Enter()
        {
            InGameManager.Instance.IsAlive = false;
            stateMachine.Controller.SetMoveEnabled(false);
            stateMachine.Player.WeaponHandler.SetAttackEnable(false);
            stateMachine.Anim.SetState(State);
        }

        public void Exit()
        {
           stateMachine.Controller.SetMoveEnabled(true);
           stateMachine.Player.WeaponHandler.SetAttackEnable(true);
        }
    }
}