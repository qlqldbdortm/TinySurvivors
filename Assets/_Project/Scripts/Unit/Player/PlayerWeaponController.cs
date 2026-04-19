using System.Collections.Generic;
using TinySurvivors.Interface;
using TinySurvivors.ObjectPool;
using TinySurvivors.Projectile;
using TinySurvivors.Weapon;
using UnityEngine;

namespace TinySurvivors.Unit.Player
{
    public class PlayerWeaponController : MonoBehaviour
    {
        [SerializeField] private Transform firePoint;
        private readonly Dictionary<WeaponInstance, List<BaseProjectile>> orbitMap = new();
        private void Awake()
        {
            if (firePoint is null)
            {
                firePoint = new GameObject("FirePoint").transform;
                firePoint.SetParent(transform);
                firePoint.position = new Vector3(0f, 0.2f, 0f);
            }
        }
        
        public void Fire(WeaponInstance weapon)
        {
            var pattern = weapon.Data.baseAttackPatternData;
            pattern.Execute(weapon, firePoint);
        }

        public void RegisterOrbit(WeaponInstance weapon, BaseProjectile obj)
        {
            if (!orbitMap.ContainsKey(weapon))
                orbitMap[weapon] = new List<BaseProjectile>();

            orbitMap[weapon].Add(obj);
        }
        
        public void FireOrbit(WeaponInstance weapon)
        {
            ClearOrbit(weapon);
            
            for (int i = 0; i < weapon.ProjectileCount; i++)
            {
                float angle = (360f / weapon.ProjectileCount * i) * Mathf.Deg2Rad;

                var obj = PoolManager.Instance.Spawn(weapon.Data.baseProjectilePrefab);

                obj.transform.SetParent(firePoint);
                obj.transform.localPosition = Vector3.zero;

                var orbit = obj.GetComponent<OrbitProjectile>();
                orbit.Setup(firePoint, weapon.AttackRange, weapon.ProjectileSpeed, weapon.Damage, angle);

                RegisterOrbit(weapon, obj);
            }
        }
        public void ClearOrbit(WeaponInstance weapon)
        {
            if (!orbitMap.TryGetValue(weapon, out var list))
                return;

            foreach (var obj in list)
            {
                PoolManager.Instance.Despawn(
                    weapon.Data.baseProjectilePrefab,
                    obj.GetComponent<BaseProjectile>()
                );
            }

            list.Clear();
        }
    }
}