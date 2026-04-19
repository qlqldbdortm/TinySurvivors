using TinySurvivors.Unit.Monster;
using UnityEngine;

namespace TinySurvivors.Projectile.Hitbox
{
    public abstract class BaseHitbox : MonoBehaviour
    {
        public abstract void OnTriggerEnter2D(Collider2D collision);
    }
}