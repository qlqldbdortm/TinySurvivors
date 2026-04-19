using TinySurvivors.Interface;
using UnityEngine;

namespace TinySurvivors.Projectile
{
    public abstract class BaseProjectile : MonoBehaviour, IPoolable
    {
        public BaseProjectile Prefab { get; private set; }

        public void SetPrefab(BaseProjectile prefab)
        {
            Prefab = prefab;
        }
        
        public abstract void Init();
        public abstract void Release();
    }
}