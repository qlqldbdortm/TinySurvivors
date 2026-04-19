using TinySurvivors.Core;
using TinySurvivors.InGame;
using TinySurvivors.UI.HUD;
using TinySurvivors.UI.WeaponSelectUI;
using TinySurvivors.Unit.Monster.Boss;
using TinySurvivors.Unit.Player;
using UnityEngine;

namespace TinySurvivors.UI
{
    public class InGameUIManager : Singleton<InGameUIManager>
    {
        [Header("Player HUD 관련 요소")]
        [SerializeField] private TimerUI timerUI;
        [SerializeField] private ExpUI expUI;
        [SerializeField] private LevelUI levelUI;
        [SerializeField] private KillCountUI killCountUI;
        [SerializeField] private WeaponUI weaponUI;
        
        [Header("Boss HUD 관련 요소")]
        [SerializeField] private BossHudUI bossHudUI;
        public BossHudUI BossHudUI => bossHudUI;
        
        [Header("Level UP 관련 요소")]
        [SerializeField] private SelectUI selectUI;
        
        [Header("엔딩 관련 요소")]
        public GameOverUI gameOverUI;
        
        protected override void Awake()
        {
            base.Awake();
            
            RefreshUI();
        }
        private void OnEnable()
        {
            if (InGameManager.Instance.Player != null)
            {
                Bind(InGameManager.Instance.Player);
            }
            else
            {
                InGameManager.Instance.OnPlayerRegistered += Bind;
            }
        }
        
        private void OnDisable()
        {
            InGameManager.Instance.OnPlayerRegistered -= Bind;
        }
        
        private void Bind(Player player)
        {
            player.PlayerExp.OnLevelUp += OnLevelUp;
            InGameManager.Instance.OnKillCountChanged += killCountUI.UpdateUI;
        }
        
        private void RefreshUI()
        {
            timerUI.Refresh();
            levelUI.Refresh();
            killCountUI.Refresh();
        }

        private void OnLevelUp()
        {
            levelUI.SetLevel();
            expUI.SetMaxValue();
            selectUI.OpenUI();
        }

        /// <summary>
        /// 보스 소환시 보스 체력바와 이름이 나오는 UI 띄우게 하는 메서드
        /// </summary>
        public void BossSpawn(BossMonster boss)
        {
            bossHudUI.BossSpawn(boss);
        }

        public void BossDespawn()
        {
            bossHudUI.BossDespawn();
        }

        public void Clear()
        {
            gameOverUI.SetClearText();
        }

        public void Defeat()
        {
            gameOverUI.SetDefeatText();
        }
    }
}