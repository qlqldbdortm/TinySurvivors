using TinySurvivors.Enum;
using TinySurvivors.Interface;
using UnityEngine;

namespace TinySurvivors.Unit.Player.State
{
    public class PlayerMoveState : IPlayerState
    {
        private readonly PlayerStateMachine stateMachine;
        public PlayerState State { get; }

        public PlayerMoveState(PlayerStateMachine stateMachine, PlayerState state)
        {
            this.stateMachine = stateMachine;
            State = state;
        }
        
        public void Enter()
        {
            stateMachine.Anim.SetState(State);
        }

        public void Exit()
        {
            
        }
    }
}