using TinySurvivors.ObjectPool;
using TinySurvivors.Unit.Monster;
using UnityEngine;

namespace TinySurvivors.Projectile.Hitbox
{
    public class RandomHitbox :BaseHitbox
    {
        public RandomProjectile random;
        public override void OnTriggerEnter2D(Collider2D collision)
        {
            if (!collision.CompareTag("Monster") || !collision.TryGetComponent(out Monster monster)) return;
            
            random.OnHit(monster);
        }
    }
}