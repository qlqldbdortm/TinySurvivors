using UnityEngine;
using UnityEngine.UI;

namespace TinySurvivors.UI.LobbyUI
{
    public class LobbyUI : MonoBehaviour
    {
        [SerializeField] private Button startButton;
        [SerializeField] private Button exitButton;

        private void Awake()
        {
            startButton.onClick.AddListener(OnClickStart);
            exitButton.onClick.AddListener(OnClickExit);
        }

        private void OnClickStart()
        {
            LobbyUIManager.Instance.ShowClassSelectUI();
        }

        private void OnClickExit()
        {
            Application.Quit();
        }
    }
}