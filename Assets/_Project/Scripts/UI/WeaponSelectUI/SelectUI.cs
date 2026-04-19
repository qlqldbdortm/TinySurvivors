using TinySurvivors.InGame;
using UnityEngine;

namespace TinySurvivors.UI.WeaponSelectUI
{
    public class SelectUI : MonoBehaviour
    {
        [SerializeField] private WeaponSelect weaponSelect;
        public void OpenUI()
        {
            gameObject.SetActive(true);
            Time.timeScale = 0f;
            weaponSelect.Open(InGameManager.Instance.Player.WeaponHandler);
        }

        public void CloseUI()
        {
            gameObject.SetActive(false);
            Time.timeScale = 1f;
        }
    }
}