using TinySurvivors.SO.UnitSO.PlayerSO;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

namespace TinySurvivors.UI.LobbyUI
{
    public class LobbyPopupUI : MonoBehaviour
    {
        [SerializeField] private Button confirmButton;
        [SerializeField] private Button cancelButton;
        [SerializeField] private TextMeshProUGUI descriptionText;
        
        private PlayerData Data { get; set; }
        private void Awake()
        {
            confirmButton.onClick.AddListener(OnConfirm);
            cancelButton.onClick.AddListener(OnCancel);
        }

        public void Show(PlayerData data)
        {
            gameObject.SetActive(true);
            
            Data = data;
            
            descriptionText.text = $"현재 선택하신 직업은 {data.baseName}입니다.\n" +
                                   $"해당 직업으로 플레이하시겠습니까?";
        }
        
        private void OnConfirm()
        {
            SceneManager.LoadScene("002.TinySurvivors-InGame");
        }

        private void OnCancel()
        {
            gameObject.SetActive(false);
        }
    }
}