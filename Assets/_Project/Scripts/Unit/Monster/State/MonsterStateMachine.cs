using System.Collections.Generic;
using _Project.Scripts.Unit.Monster.Boss;
using TinySurvivors.Enum;
using TinySurvivors.Interface;
using TinySurvivors.Unit.Monster.Boss;
using UnityEngine;

namespace TinySurvivors.Unit.Monster.State
{
    public class MonsterStateMachine : MonoBehaviour
    {
        public Monster Monster { get; private set; }
        public UnitAnimator Anim { get; private set; }
        
        private Dictionary<MonsterState, IMonsterState> states;
        
        private IMonsterState currentState;
        private MonsterState ms;
        private void Awake()
        {
            Monster ??= GetComponent<Monster>();
            Anim ??= GetComponentInChildren<UnitAnimator>();

            states = new Dictionary<MonsterState, IMonsterState>
            {
                { MonsterState.None, new MonsterNoneState(this,MonsterState.None) },
                { MonsterState.Idle, new MonsterIdleState(this, MonsterState.Idle) },
                { MonsterState.Chase, new MonsterChaseState(this, MonsterState.Chase) },
                { MonsterState.Attack, new MonsterAttackState(this, MonsterState.Attack) },
                { MonsterState.Damaged, new MonsterDamageState(this, MonsterState.Damaged) },
                { MonsterState.Death, new MonsterDeathState(this, MonsterState.Death) },
                { MonsterState.BossPattern , new BossPatternState(this, MonsterState.BossPattern)},
                { MonsterState.BossDash, new BossDashState(this, MonsterState.BossDash) },
                { MonsterState.BossChase, new BossChaseState(this, MonsterState.BossChase) },
                { MonsterState.BossBullet, new BossBulletState(this, MonsterState.BossBullet) },
            };
        }

        private void Start()
        {
            ChangeState(MonsterState.Idle);
        }

        private void Update()
        {
            currentState?.Update();
        }
        
        public void ChangeState(MonsterState newState)
        {
            if (currentState?.State == newState && newState != MonsterState.Attack) return;
            
            currentState?.Exit();
            currentState = states[newState];
            ms = states[newState].State;
            currentState.Enter();
        }
    }
}