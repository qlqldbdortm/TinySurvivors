using TinySurvivors.Unit.Player;
using UnityEngine;

namespace TinySurvivors.Item
{
    public class ItemColliderHandler : MonoBehaviour
    {
        public BaseItem item;
        private void OnTriggerEnter2D(Collider2D collision)
        {
            if (!collision.TryGetComponent<PlayerColliderHandler>(out var player)) return;
            
            item.Use(player.BasePlayer);
        }
    }
}