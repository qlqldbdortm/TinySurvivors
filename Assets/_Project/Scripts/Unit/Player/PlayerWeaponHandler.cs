using System;
using System.Collections.Generic;
using TinySurvivors.Enum;
using TinySurvivors.ObjectPool;
using TinySurvivors.Projectile;
using TinySurvivors.SO.UnitSO.PlayerSO;
using TinySurvivors.SO.WeaponSO;
using TinySurvivors.Weapon;
using UnityEngine;

namespace TinySurvivors.Unit.Player
{
    public class PlayerWeaponHandler : MonoBehaviour
    {
        private readonly List<WeaponInstance> equippedWeapons = new();
        private PlayerWeaponController WeaponController { get; set; }
        public int WeaponCount => equippedWeapons.Count;
        public List<WeaponInstance> CurrentWeapons => equippedWeapons;
        public event Action<WeaponInstance> OnWeaponAdded;
        public event Action<WeaponInstance> OnWeaponLevelUp;
        public event Action<List<WeaponInstance>> OnWeaponListChanged;

        private bool canAttack;
        
        private void Update()
        {
            if (!canAttack) return;
            
            foreach (var weapon in equippedWeapons)
            {
                // 플레이어 주변을 회전하는 무기는 한번 호출하면 호출 X.
                if (weapon.Data.baseAttackType == AttackType.Orbit)
                    continue;

                weapon.Cooldown -= Time.deltaTime;

                if (weapon.Cooldown <= 0f)
                {
                    WeaponController.Fire(weapon);
                    weapon.Cooldown = weapon.Interval;
                }
            }
        }
        public void Init(PlayerData playerData, PlayerWeaponController weaponController)
        {
            WeaponController = weaponController;
            AddWeapon(playerData.baseWeaponData);
        }
        
        /// <summary>
        /// 무기를 가지고 있는지 확인하는 메서드
        /// </summary>
        public bool HasWeapon(WeaponData data) => equippedWeapons.Exists(w => w.Data == data);

        /// <summary>
        /// 무기가 최대레벨에 도달했는지 확인하는 메서드
        /// </summary>
        public bool IsWeaponMaxLevel(WeaponData data)
        {
            var weapon = equippedWeapons.Find(w => w.Data == data);
            return weapon != null && weapon.IsMaxLevel;
        }

        /// <summary>
        /// 현재 무기의 레벨을 반환하는 메서드 <br/>
        /// 무기가 존재하면 무기의 레벨을 반환하고 없으면 0을 반환
        /// </summary>
        public int GetCurrentWeaponLevel(WeaponData data)
        {
            var weapon = equippedWeapons.Find(w => w.Data == data);
            return weapon?.Level ?? 0;
        }
        
        /// <summary>
        /// 무기를 추가하는 메서드
        /// </summary>
        /// <param name="data"></param>
        public void AddWeapon(WeaponData data)
        {
            var existing = equippedWeapons.Find(weapon => weapon.Data == data);
            
            if (existing != null)
            {
                existing.WeaponLevelUp();
                OnWeaponLevelUp?.Invoke(existing);
                
                if (existing.Data.baseAttackType == AttackType.Orbit)
                {
                    WeaponController.FireOrbit(existing);
                }

                OnWeaponListChanged?.Invoke(equippedWeapons);
                return;
            }

            // 무기가 6개면 최대 보유 수량에 도달한 것이기 때문에 이후에 무기를 추가할 필요가 없음.
            // 위쪽에서 Weapon의 레벨만 올리고 반환
            if (equippedWeapons.Count >= 6) return;
            
            var weapon = new WeaponInstance(data);
            
            equippedWeapons.Add(weapon);
            
            PoolManager.Instance.CreatePool(
                weapon.Data.baseProjectilePrefab,
                weapon.Data.capacity, 
                new GameObject(weapon.Data.weaponName).transform);
            
            if (weapon.Data.baseAttackType == AttackType.Orbit)
            {
                WeaponController.FireOrbit(weapon);
            }
            
            OnWeaponAdded?.Invoke(weapon);
            OnWeaponListChanged?.Invoke(equippedWeapons);
        }

        public void SetAttackEnable(bool enable)
        {
            canAttack = enable;
            
            if (canAttack) return;
            
            foreach (var weapon in equippedWeapons)
            {
                if (weapon.Data.baseProjectilePrefab is OrbitProjectile)
                {
                    WeaponController.ClearOrbit(weapon);       
                }
            }
        }
    }
}