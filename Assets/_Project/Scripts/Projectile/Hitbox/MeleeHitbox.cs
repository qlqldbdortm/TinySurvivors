using TinySurvivors.Unit.Monster;
using UnityEngine;

namespace TinySurvivors.Projectile.Hitbox
{
    public class MeleeHitbox : BaseHitbox
    {
        public MeleeProjectile melee;
        public override void OnTriggerEnter2D(Collider2D collision)
        {
            if (!collision.CompareTag("Monster") || !collision.TryGetComponent<Monster>(out var monster)) return;
            if (!melee.HitTargets.Add(monster)) return;
            
            melee.OnHit(monster);
        }
    }
}