using TinySurvivors.Core;
using TinySurvivors.SO.UnitSO.PlayerSO;
using TinySurvivors.UI.LobbyUI;
using UnityEngine;

namespace TinySurvivors.UI
{
    public class LobbyUIManager : Singleton<LobbyUIManager>
    {
        [SerializeField] private LobbyUI.LobbyUI lobbyUI;
        [SerializeField] private ClassSelectUI classSelectUI;
        [SerializeField] private LobbyPopupUI lobbyPopupUI;
        
        public void ShowClassSelectUI()
        {
            classSelectUI.OpenUI();
        }
        
        public void ShowPopup(PlayerData data)
        {
            lobbyPopupUI.Show(data);
        }
    }
}