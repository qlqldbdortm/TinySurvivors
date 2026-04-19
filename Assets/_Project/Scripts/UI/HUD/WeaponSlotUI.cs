using TinySurvivors.Weapon;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace TinySurvivors.UI.HUD
{
    public class WeaponSlotUI : MonoBehaviour
    {
        [SerializeField] private Image weaponIcon;
        [SerializeField] private TextMeshProUGUI levelText;

        public WeaponInstance Weapon { get; private set; }
        
        public void Init(WeaponInstance weapon)
        {
            Weapon = weapon;
            
            weaponIcon.sprite = weapon.Data.weaponIcon;
            Refresh();
        }

        public void Refresh()
        {
            levelText.text = $"Lv {Weapon.Level}";
        }
    }
}