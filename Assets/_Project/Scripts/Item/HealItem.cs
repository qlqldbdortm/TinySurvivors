using TinySurvivors.ObjectPool;
using TinySurvivors.Unit.Player;
using UnityEngine;

namespace TinySurvivors.Item
{
    public class HealItem : BaseItem
    {
        [SerializeField] private float healAmount;
        
        public override void Use(Player player)
        {
            player.UnitHp.Heal(healAmount);
            PoolManager.Instance.Despawn(Prefab, this);
        }

        public override void Init()
        {
            
        }

        public override void Release()
        {
            
        }
    }
}