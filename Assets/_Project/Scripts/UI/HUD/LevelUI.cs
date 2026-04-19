using TinySurvivors.InGame;
using TinySurvivors.Unit.Player;
using TMPro;
using UnityEngine;

namespace TinySurvivors.UI.HUD
{
    public class LevelUI : MonoBehaviour
    {
        [SerializeField] private TextMeshProUGUI levelText;

        private void OnEnable()
        {
            if (InGameManager.Instance.Player is not null)
                Bind(InGameManager.Instance.Player);
            else
                InGameManager.Instance.OnPlayerRegistered += Bind;
        }

        private void Bind(Player player)
        {
            levelText.text = $"Lv. {player.PlayerExp.Level}";
            player.PlayerExp.OnLevelUp += SetLevel;
        }
        
        public void SetLevel()
        {
            levelText.text = $"Lv. {InGameManager.Instance.Player.PlayerExp.Level}";
        }

        public void Refresh()
        {
            levelText.text = "Lv. 1";
        }
    }
}