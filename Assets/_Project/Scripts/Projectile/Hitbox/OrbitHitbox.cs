using TinySurvivors.Unit.Monster;
using UnityEngine;

namespace TinySurvivors.Projectile.Hitbox
{
    public class OrbitHitbox : BaseHitbox
    {
        public OrbitProjectile orbit;

        public override void OnTriggerEnter2D(Collider2D collision)
        {
            if (!collision.CompareTag("Monster") || !collision.TryGetComponent<Monster>(out var monster)) return;

            orbit.OnHit(monster);
        }
    }
}