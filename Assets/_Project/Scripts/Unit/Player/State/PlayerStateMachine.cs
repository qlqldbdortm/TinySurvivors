using System;
using System.Collections.Generic;
using TinySurvivors.Enum;
using TinySurvivors.Interface;
using UnityEngine;
using UnityEngine.InputSystem;

namespace TinySurvivors.Unit.Player.State
{
    public class PlayerStateMachine : MonoBehaviour
    {
        public Player Player { get; private set; }
        public UnitAnimator Anim { get; private set; }
        public PlayerInputController Controller { get; private set; }

        private Dictionary<PlayerState, IPlayerState> states;
        private IPlayerState currentState;

        private PlayerState ps;
        private void Awake()
        {
            Player = GetComponent<Player>();
            Anim = GetComponentInChildren<UnitAnimator>();
            Controller = GetComponent<PlayerInputController>();

            states = new Dictionary<PlayerState, IPlayerState>
            {
                { PlayerState.Idle, new PlayerIdleState(this, PlayerState.Idle) },
                { PlayerState.Move, new PlayerMoveState(this, PlayerState.Move) },
                { PlayerState.Damage, new PlayerDamageState(this, PlayerState.Damage) },
                { PlayerState.Death, new PlayerDeathState(this, PlayerState.Death) }
            };
        }
        
        private void Start()
        {
            ChangeState(PlayerState.Idle);
        }

        public void ChangeState(PlayerState newState)
        {
            if (currentState?.State == newState)
                return;

            if (currentState?.State == PlayerState.Death)
                return;

            currentState?.Exit();
            currentState = states[newState];
            ps = states[newState].State;
            currentState.Enter();
        }

        /// <summary>
        /// Damage State로 넘어가기 위한 이벤트 구독용 메서드
        /// </summary>
        public void OnDamaged(float damage)
        {
            ChangeState(PlayerState.Damage);
        }

        /// <summary>
        /// Death State로 넘어가기 위한 이벤트 구독용 메서드
        /// </summary>
        public void OnDeath()
        {
            ChangeState(PlayerState.Death);
        }
    }
}