using System.Collections.Generic;
using TinySurvivors.Extensions;
using TinySurvivors.SO.RewardSO;
using TinySurvivors.SO.WeaponSO;
using TinySurvivors.Unit.Player;
using UnityEngine;

namespace TinySurvivors.UI.WeaponSelectUI
{
    public class WeaponSelect : MonoBehaviour
    {
        [SerializeField] private List<WeaponData> database;
        [SerializeField] private SelectableUI[] slots;
        [SerializeField] private HealthRewardData healthReward;
        public void Open(PlayerWeaponHandler handler)
        {
            var weaponList = new List<WeaponData>();

            foreach (var data in database)
            {
                if (!handler.HasWeapon(data) || !handler.IsWeaponMaxLevel(data))
                    weaponList.Add(data);
            }

            weaponList.Shuffle();

            int weaponIndex = 0;
            
            foreach (var slot in slots)
            {
                if (weaponIndex < weaponList.Count)
                {
                    slot.Init(weaponList[weaponIndex], handler);
                    slot.gameObject.SetActive(true);
                    weaponIndex++;
                }
                else
                {
                    slot.InitHealth(healthReward);
                    slot.gameObject.SetActive(true);
                }
            }
        }
    }
}