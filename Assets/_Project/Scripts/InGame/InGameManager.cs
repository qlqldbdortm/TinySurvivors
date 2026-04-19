using System;
using Cinemachine;
using TinySurvivors.Core;
using TinySurvivors.UI;
using TinySurvivors.Unit.Player;
using TinySurvivors.Utility;
using UnityEngine;

namespace TinySurvivors.InGame
{
    public class InGameManager : Singleton<InGameManager>
    {
        [SerializeField] private CinemachineVirtualCamera virtualCamera;
        public Player Player { get; set; }
        public bool IsAlive { get; set; }
        public Timer Timer { get; private set; } = new();

        
        public event Action<Player> OnPlayerRegistered;
        
        private int killCount;

        public int KillCount
        {
            get => killCount;
            set
            {
                killCount = value;
                OnKillCountChanged?.Invoke(killCount);
            }
        }

        public event Action<int> OnKillCountChanged;

        protected override void Awake()
        {
            base.Awake();

            Time.timeScale = 1;
            Timer.Start();
        }

        private void Start()
        {
           
        }
        
        public void RegisterPlayer(Player player)
        {
            Player = player;
            OnPlayerRegistered?.Invoke(player);
            
            if (Player != null)
            {
                Player.AnimationEventHandler.OnPlayerDeath += PlayerIsDead;
            }
            
            virtualCamera.Follow = Player.transform;
        }

        private void PlayerIsDead()
        {
            InGameUIManager.Instance.gameOverUI.Setup();
        }

        public void GameClear()
        {
            InGameUIManager.Instance.gameOverUI.Setup();
        }
        
        /// <summary>
        /// 플레이어가 죽었는지 살아있는지 체크하는 메서드
        /// </summary>
        /// <returns></returns>
        public static bool PlayerIsAlive()
        {
            return Instance && Instance.IsAlive;
        }
    }
}