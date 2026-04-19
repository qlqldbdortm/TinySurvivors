using TinySurvivors.InGame;
using TinySurvivors.Unit.Player;
using UnityEngine;
using UnityEngine.UI;

namespace TinySurvivors.UI.HUD
{
    public class ExpUI : MonoBehaviour
    {
        [SerializeField] private Slider expGauge;
        
        private void OnEnable()
        {
            expGauge.value = 0;

            if (InGameManager.Instance.Player is not null)
                Bind(InGameManager.Instance.Player);
            else
                InGameManager.Instance.OnPlayerRegistered += Bind;
        }
        private void Bind(Player player)
        {

            expGauge.maxValue = player.PlayerExp.MaxExp;

            player.PlayerExp.OnValueChanged += SetValue;
            player.PlayerExp.OnLevelUp += SetMaxValue;
        }
        public void SetValue(float value)
        {
            expGauge.value = value;
        }

        public void SetMaxValue()
        {
            expGauge.maxValue = InGameManager.Instance.Player.PlayerExp.MaxExp;
        }
    }
}