using TinySurvivors.Enum;
using TinySurvivors.InGame;
using TinySurvivors.Interface;
using TinySurvivors.SO.UnitSO.MonsterSO;
using TinySurvivors.Unit.Monster.Boss;
using TinySurvivors.Unit.Monster.State;
using UnityEngine;

namespace TinySurvivors.Unit.Monster
{
    [RequireComponent(typeof(MonsterController),typeof(MonsterStateMachine),typeof(MonsterColliderHandler))]
    public class Monster : Unit, IPoolable
    {
        public virtual bool IsBoss => false;
        /// <summary>
        /// 몬스터가 추적할 대상 (Tag가 Player인 오브젝트)
        /// </summary>
        public Transform Target { get; private set; }
        public Monster Prefab { get; private set; }
        public MonsterController MonsterController { get; private set; }
        public MonsterStateMachine MonsterStateMachine { get; private set; }
        public MonsterStat MonsterStat { get; private set; }
        public MonsterAttack MonsterAttack { get; private set; }
        public MonsterSpriteController SpriteController { get; private set; }
        public MonsterAnimationEventHandler MonsterAnimationEventHandler { get; private set; }
        public MonsterColliderHandler MonsterColliderHandler { get; private set; }
        protected override void Awake()
        {
            var baseMonsterData = baseUnitData as MonsterData;
            
            MonsterStat = new MonsterStat(baseMonsterData);
            UnitHp = new UnitHp(MonsterStat);

            MonsterController = GetComponent<MonsterController>();
            MonsterStateMachine = GetComponent<MonsterStateMachine>();
            MonsterColliderHandler = GetComponent<MonsterColliderHandler>();
            MonsterAnimationEventHandler = GetComponentInChildren<MonsterAnimationEventHandler>();
            SpriteController = GetComponent<MonsterSpriteController>();
            MonsterAnimationEventHandler.Init(this);
            MonsterController.Init(this);
            MonsterColliderHandler.Init(this);
        }
        
        private void Start()
        {
            if (InGameManager.Instance.Player != null)
            {
                SetTarget(InGameManager.Instance.Player);
            }
            else
            {
                InGameManager.Instance.OnPlayerRegistered += SetTarget;
            }
        }
        
        // private void Start()
        // {
        //     Target = InGameManager.Instance.Player.transform;
        //     
        //     // 몬스터의 공격 타입이 원거리 공격이라면 MonsterAttack, EventHandler를 찾음
        //     if (MonsterStat.Data?.baseAttackType != AttackType.Range) return;
        //     MonsterAttack = GetComponent<MonsterAttack>();
        //     MonsterAttack.Init(Target, this);
        // }
        
        private void OnEnable()
        {
            PlayerSpawner.OnPlayerSpawned += SetTarget;
        }

        private void OnDisable()
        {
            PlayerSpawner.OnPlayerSpawned -= SetTarget;
        }
        public virtual void Init()
        {
            MonsterManager.Instance.Register(this);
            MonsterStateMachine?.ChangeState(MonsterState.Idle);
            if (UnitHp is not null)
            {
                UnitHp.Refresh(); // Pool에 들어갔다가 나올 때 체력을 다시 세팅해줌
                UnitHp.OnDeath += OnDeath;
                UnitHp.OnDamaged += OnDamaged;
            }
        }

        public virtual void Release()
        {
            MonsterManager.Instance.Unregister(this);
            MonsterStateMachine?.ChangeState(MonsterState.None);
            if (UnitHp is not null)
            {
                UnitHp.OnDeath -= OnDeath;
                UnitHp.OnDamaged -= OnDamaged;
            }
        }
        
        public float DistanceToTarget()
        {
            return Vector2.Distance(transform.position, Target.position);
        }

        public void SetPrefab(Monster prefab)
        {
            Prefab = prefab;
        }
        
        private void OnDeath()
        {
            MonsterStateMachine.ChangeState(MonsterState.Death);
        }

        private void OnDamaged(float damage)
        {
            // 보스 몬스터라면 데미지 상태로 넘어가지 않게
            if (this is BossMonster) return;
            MonsterStateMachine.ChangeState(MonsterState.Damaged);
        }
        
        private void SetTarget(Player.Player player)
        {
            Target = player.transform;

            if (MonsterStat.Data?.baseAttackType == AttackType.Range)
            {
                MonsterAttack = GetComponent<MonsterAttack>();
                MonsterAttack.Init(Target, this);
            }
        }
    }
}