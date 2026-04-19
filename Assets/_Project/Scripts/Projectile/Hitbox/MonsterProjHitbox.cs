using TinySurvivors.ObjectPool;
using TinySurvivors.Unit.Player;
using UnityEngine;

namespace TinySurvivors.Projectile.Hitbox
{
    public class MonsterProjHitbox : BaseHitbox
    {
        public MonsterProjectile proj;
        public override void OnTriggerEnter2D(Collider2D collision)
        {
            if (!collision.TryGetComponent<PlayerColliderHandler>(out var player)) return;

            proj.OnHit(player);

        }
    }
}