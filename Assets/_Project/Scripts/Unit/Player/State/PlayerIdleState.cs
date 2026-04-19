using TinySurvivors.Enum;
using TinySurvivors.Interface;
using UnityEngine;

namespace TinySurvivors.Unit.Player.State
{
    public class PlayerIdleState : IPlayerState
    {
        private readonly PlayerStateMachine stateMachine;
        public PlayerState State { get; }

        public PlayerIdleState(PlayerStateMachine stateMachine, PlayerState state)
        {
            this.stateMachine = stateMachine;
            State = state;
        }
        
        public void Enter()
        {
            if (stateMachine.Controller.MoveDir != Vector2.zero)
            {
                stateMachine.Anim.SetState(PlayerState.Move);    
            }
            
            stateMachine.Controller.SetMoveEnabled(true);
            stateMachine.Player.WeaponHandler.SetAttackEnable(true);
            stateMachine.Anim.SetState(State);
        }

        public void Exit()
        {
            
        }
    }
}