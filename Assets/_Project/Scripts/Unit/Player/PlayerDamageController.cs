using UnityEngine;

namespace TinySurvivors.Unit.Player
{
    public class PlayerDamageController
    {
        private readonly float hitCooldown;
        private float lastHitTime;

        public PlayerDamageController(float hitCooldown)
        {
            this.hitCooldown = hitCooldown;
        }
        
        public bool CanTakeDamage()
        {
            return Time.time >= lastHitTime + hitCooldown;
        }

        public void OnDamaged()
        {
            lastHitTime = Time.time;
        }
    }
}