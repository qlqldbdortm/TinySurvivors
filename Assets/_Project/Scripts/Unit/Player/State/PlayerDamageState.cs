using Cysharp.Threading.Tasks;
using TinySurvivors.Enum;
using TinySurvivors.Interface;

namespace TinySurvivors.Unit.Player.State
{
    public class PlayerDamageState : IPlayerState
    {
        private readonly PlayerStateMachine stateMachine;
        public PlayerState State { get; }
        
        public PlayerDamageState(PlayerStateMachine stateMachine, PlayerState state)
        {
            this.stateMachine = stateMachine;
            State = state;
        }
        
        public void Enter()
        {
            
        }
        
        public void Exit()
        {
            
        }
    }
}