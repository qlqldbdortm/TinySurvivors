using TinySurvivors.InGame;
using TinySurvivors.SO.UnitSO;
using TinySurvivors.SO.UnitSO.PlayerSO;
using TinySurvivors.UI;
using TinySurvivors.UI.PlayerUI;
using TinySurvivors.Unit.Player.State;
using UnityEngine;

namespace TinySurvivors.Unit.Player
{
    public class Player : Unit
    {
        private PlayerInputHandler PlayerInputHandler { get; set; }
        private PlayerInputController PlayerInputController { get; set; }
        public PlayerColliderHandler ColliderHandler { get; private set; }
        public PlayerStateMachine StateMachine { get; private set; }
        public PlayerWeaponHandler WeaponHandler { get; private set; }
        public PlayerWeaponController WeaponController { get; private set; }
        public PlayerAnimationEventHandler AnimationEventHandler { get; private set; }
        public PlayerStat PlayerStat { get; set; }
        public PlayerExp PlayerExp { get; set; }
        public PlayerHpUI PlayerHpUI { get; private set; }
        public PlayerDamageController DamageController { get; private set; }
        protected override void Awake()
        {
            base.Awake();

            InGameManager.Instance.Player = this;
            InGameManager.Instance.IsAlive = true;
            
            PlayerInputHandler ??= GetComponent<PlayerInputHandler>();
            PlayerInputController ??= GetComponent<PlayerInputController>();
            ColliderHandler = GetComponentInChildren<PlayerColliderHandler>();
            StateMachine = GetComponent<PlayerStateMachine>();
            WeaponHandler = GetComponent<PlayerWeaponHandler>();
            WeaponController = GetComponent<PlayerWeaponController>();
            AnimationEventHandler = GetComponentInChildren<PlayerAnimationEventHandler>();
            PlayerExp = GetComponent<PlayerExp>();
            PlayerHpUI = GetComponentInChildren<PlayerHpUI>();
        }
        
        public override void SetUnitData(UnitData data)
        {
            base.SetUnitData(data);

            var playerData = data as PlayerData;
            Debug.Assert(playerData != null, "플레이어 데이터가 없음");
            
            PlayerStat = new PlayerStat(playerData);
            UnitHp = new UnitHp(PlayerStat);
            DamageController = new PlayerDamageController(PlayerStat.InvincibleTime);

            PlayerInputController.Init(this);
            ColliderHandler.Init(this);
            WeaponHandler.Init(playerData, WeaponController);
            PlayerHpUI.Init(UnitHp.MaxHp);
            
            UnitHp.OnDamaged += PlayerHpUI.SetHpBar;
            UnitHp.OnDamaged += StateMachine.OnDamaged;
            UnitHp.OnHealed += PlayerHpUI.SetHpBar;
            UnitHp.OnDeath += StateMachine.OnDeath;
            UnitHp.OnDeath += InGameUIManager.Instance.Defeat;
            UnitHp.OnMaxHpChanged += PlayerHpUI.SetMaxHpBar;
        }

        private void OnEnable()
        {
            PlayerInputHandler.OnMove += PlayerInputController.OnMove;
        }

        private void OnDisable()
        {
            PlayerInputHandler.OnMove -= PlayerInputController.OnMove;
            UnitHp.OnDamaged -= PlayerHpUI.SetHpBar;
            UnitHp.OnDamaged -= StateMachine.OnDamaged;
            UnitHp.OnHealed -= PlayerHpUI.SetHpBar;
            UnitHp.OnDeath -= StateMachine.OnDeath;
            UnitHp.OnDeath -= InGameUIManager.Instance.Defeat;
            UnitHp.OnMaxHpChanged -= PlayerHpUI.SetMaxHpBar;
        }
    }
}