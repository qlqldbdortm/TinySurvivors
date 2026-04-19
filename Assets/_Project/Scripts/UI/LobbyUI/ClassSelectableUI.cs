using TinySurvivors.Core;
using TinySurvivors.SO.UnitSO.PlayerSO;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace TinySurvivors.UI.LobbyUI
{
    public class ClassSelectableUI : MonoBehaviour
    {
        [SerializeField] private Button button;
        [SerializeField] private Image classImage;
        [SerializeField] private TextMeshProUGUI classNameText;
        [SerializeField] private TextMeshProUGUI classDescriptionText;

        private int Index { get; set; }
        private PlayerData Data { get; set; }

        private void Awake()
        {
            button.onClick.AddListener(OnSelect);            
        }
        
        public void Init(PlayerDatabase database, int index)
        {
            Data = database.playerData;
            Index = index;
            
            classImage.sprite = Data.baseImage;
            classNameText.text = Data.baseName;
            classDescriptionText.text = Data.baseDescription;
        }
        
        private void OnSelect()
        {
            GameManager.Instance.SelectPlayer(Index);
            LobbyUIManager.Instance.ShowPopup(Data);
        }
    }
}