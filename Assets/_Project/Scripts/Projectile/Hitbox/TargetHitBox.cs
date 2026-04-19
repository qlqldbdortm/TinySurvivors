using TinySurvivors.ObjectPool;
using TinySurvivors.Unit.Monster;
using UnityEngine;

namespace TinySurvivors.Projectile.Hitbox
{
    public class TargetHitBox : BaseHitbox
    {
        public TargetProjectile target;
        public override void OnTriggerEnter2D(Collider2D collision)
        {
            if (!collision.CompareTag("Monster") || !collision.TryGetComponent(out Monster monster)) return;

            target.OnHit(monster);
        }
    }
}