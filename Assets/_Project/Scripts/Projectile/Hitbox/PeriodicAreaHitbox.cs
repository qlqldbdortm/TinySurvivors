using TinySurvivors.Unit.Monster;
using UnityEngine;

namespace TinySurvivors.Projectile.Hitbox
{
    public class PeriodicAreaHitbox : BaseHitbox
    {
        public PeriodicAreaProjectile periodic;
        public override void OnTriggerEnter2D(Collider2D collision)
        {
            if (!collision.CompareTag("Monster") || !collision.TryGetComponent(out Monster monster)) return;
            periodic.OnHit(monster);
        }
    }
}