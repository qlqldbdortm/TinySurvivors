using TinySurvivors.InGame;
using TinySurvivors.ObjectPool;
using TinySurvivors.Unit.Player;

namespace TinySurvivors.Item
{
    public class MagnetItem : BaseItem
    {
        public override void Use(Player player)
        {
            foreach (var exp in DropManager.Instance.ActiveExps)
            {
                exp.Magnet(player.transform);
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