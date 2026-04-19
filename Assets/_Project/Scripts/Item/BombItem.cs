using System.Collections.Generic;
using TinySurvivors.InGame;
using TinySurvivors.ObjectPool;
using TinySurvivors.Unit.Monster;
using TinySurvivors.Unit.Player;

namespace TinySurvivors.Item
{
    public class BombItem : BaseItem
    {
        public override void Use(Player player)
        {
            var monsters = new List<Monster>(MonsterManager.Instance.Enemies);
            
            foreach (var monster in monsters)
            {
                if (monster.IsBoss) continue;
                
                monster.UnitHp.TakeDamage(float.MaxValue);
            }
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