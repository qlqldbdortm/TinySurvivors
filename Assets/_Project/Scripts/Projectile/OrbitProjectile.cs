using TinySurvivors.Unit.Monster;
using UnityEngine;

namespace TinySurvivors.Projectile
{
    /// <summary>
    /// 플레이어 중심을 기준으로 회전하는 공격
    /// </summary>
    public class OrbitProjectile : BaseProjectile
    {
        public float Damage { get; private set; }

        private Transform Target { get; set; }
        private float Radius { get; set; }
        private float Speed { get; set; }
        private float Angle { get; set; }

        private void Update()
        {
            Angle += Speed * Time.deltaTime;
            transform.position = Target.position + new Vector3(Mathf.Cos(Angle), Mathf.Sin(Angle)) * Radius;
        }

        public override void Init()
        {
        }

        public override void Release()
        {
        }

        public void Setup(Transform origin, float weaponAttackRange, float weaponProjectileSpeed, float weaponDamage,
            float angle)
        {
            Target = origin;
            Radius = weaponAttackRange;
            Speed = weaponProjectileSpeed;
            Damage = weaponDamage;
            Angle = angle;
        }

        public void OnHit(Monster monster)
        {
            monster.UnitHp.TakeDamage(Damage);
        }
    }
}