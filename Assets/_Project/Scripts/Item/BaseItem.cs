using TinySurvivors.Enum;
using TinySurvivors.Interface;
using TinySurvivors.Unit.Player;
using UnityEngine;

namespace TinySurvivors.Item
{
    public abstract class BaseItem : MonoBehaviour, IPoolable
    {
        public ItemType itemType;
        public BaseItem Prefab { get; private set; }
        public abstract void Use(Player player);
        public abstract void Init();
        public abstract void Release();
        
        public void SetPrefab(BaseItem prefab)
        {
            Prefab = prefab;
        }

    }
}