using TinySurvivors.Core;
using TMPro;
using UnityEngine;

namespace TinySurvivors.UI.HUD
{
    public class KillCountUI : MonoBehaviour
    {
        [SerializeField] private TextMeshProUGUI killCountText;

        public void Refresh()
        {
            killCountText.text = "0";
        }

        public void UpdateUI(int value)
        {
            killCountText.text = $"{value}";
        }
    }
}