using System.Collections.Generic;
using TinySurvivors.InGame;
using TinySurvivors.Unit.Player;
using TinySurvivors.Weapon;
using UnityEngine;

namespace TinySurvivors.UI.HUD
{
    public class WeaponUI : MonoBehaviour
    {
        [SerializeField] private WeaponSlotUI slotPrefab;
        [SerializeField] private Transform slotParent;
        
        private readonly List<WeaponSlotUI> slots = new();
        private PlayerWeaponHandler handler;
        private void Start()
        {
            if (InGameManager.Instance.Player is not null)
                Bind(InGameManager.Instance.Player);
            else
                InGameManager.Instance.OnPlayerRegistered += Bind;
        }
        
        private void OnEnable()
        {
            if (InGameManager.Instance?.Player != null)
            {
                Bind(InGameManager.Instance.Player);
            }
            else
            {
                if (InGameManager.Instance != null)
                {
                    InGameManager.Instance.OnPlayerRegistered += Bind;
                }
            }
        }

        private void OnDisable()
        {
            if (InGameManager.Instance != null)
            {
                InGameManager.Instance.OnPlayerRegistered -= Bind;
            }

            Unbind();
        }
        private void Bind(Player player)
        {
            handler = player.WeaponHandler;
            RefreshAll(handler.CurrentWeapons);

            handler.OnWeaponAdded += OnWeaponAdded;
            handler.OnWeaponLevelUp += OnWeaponLevelUp;
            handler.OnWeaponListChanged += RefreshAll;
        }

        private void Unbind()
        {
            handler.OnWeaponAdded -= OnWeaponAdded;
            handler.OnWeaponLevelUp -= OnWeaponLevelUp;
            handler.OnWeaponListChanged -= RefreshAll;
        }
        
        private void OnWeaponAdded(WeaponInstance weapon)
        {
            var slot = Instantiate(slotPrefab, slotParent);
            slot.Init(weapon);
            slots.Add(slot);
        }
        private void OnWeaponLevelUp(WeaponInstance weapon)
        {
            var slot = slots.Find(s => s.Weapon == weapon);
            slot?.Refresh();
        }
        private void RefreshAll(List<WeaponInstance> weapons)
        {
            for (int i = 0; i < weapons.Count; i++)
            {
                if (i >= slots.Count)
                {
                    var slot = Instantiate(slotPrefab, slotParent);
                    slots.Add(slot);
                }

                slots[i].Init(weapons[i]);
                slots[i].gameObject.SetActive(true);
            }

            for (int i = weapons.Count; i < slots.Count; i++)
            {
                slots[i].gameObject.SetActive(false);
            }
        }
    }
}