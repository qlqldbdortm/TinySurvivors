using TinySurvivors.InGame;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

namespace TinySurvivors.UI
{
    public class GameOverUI : MonoBehaviour
    {
        [SerializeField] private TextMeshProUGUI endingText;
        [SerializeField] private TextMeshProUGUI survivalTimeText;
        [SerializeField] private TextMeshProUGUI monsterKillCountText;
        [SerializeField] private TextMeshProUGUI playerLevelText;
        [SerializeField] private Button restartButton;
        [SerializeField] private Button lobbyButton;

        private void Awake()
        {
            lobbyButton.onClick.AddListener(GoLobby);
            restartButton.onClick.AddListener(Restart);
        }
        
        public void Setup()
        {
            Time.timeScale = 0;
            
            SurviveTime();
            monsterKillCountText.text = $"{InGameManager.Instance.KillCount}";
            playerLevelText.text = $"{InGameManager.Instance.Player.PlayerExp.Level}";
            
            gameObject.SetActive(true);
        }
        
        private void SurviveTime()
        {
            float time = InGameManager.Instance.Timer.NowTime;
            int minutes = (int)(time / 60);
            int seconds = (int)(time % 60);
            survivalTimeText.text = $"{minutes:00} : {seconds:00}";
        }

        private void GoLobby()
        {
            SceneManager.LoadScene("001.TinySurvivors-Lobby");
        }

        private void Restart()
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        }

        public void SetClearText()
        {
            endingText.text = "생존 성공";
        }

        public void SetDefeatText()
        {
            endingText.text = "생존 실패";
        }
    }
}