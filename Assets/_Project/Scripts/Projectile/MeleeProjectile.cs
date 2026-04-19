using System.Collections.Generic;
using DG.Tweening;
using TinySurvivors.ObjectPool;
using TinySurvivors.Unit.Monster;
using UnityEngine;

namespace TinySurvivors.Projectile
{
    /// <summary>
    /// 플레이어기준 가장 가까운 적이 있는 곳으로 휘두르는 공격 
    /// </summary>
    public class MeleeProjectile : BaseProjectile
    {
        /// <summary>
        /// 무기를 휘두르는 중심점 
        /// </summary>
        [SerializeField] private Transform pivot;

        /// <summary>
        /// 피격 시 대상에게 주는 피해량
        /// </summary>
        public float Damage { get; private set; }

        /// <summary>
        /// 히트박스가 유지되는 시간 
        /// </summary>
        private float Lifetime { get; set; }

        /// <summary>
        /// 무기를 휘두르는 각도
        /// </summary>
        private float Angle { get; set; }

        /// <summary>
        /// 가장 가까운 적이 있는 방향 각도
        /// </summary>
        private float BaseRotation { get; set; }

        /// <summary>
        /// pivot으로 부모를 옮기기 전 부모
        /// </summary>
        private Transform Parent { get; set; }

        /// <summary>
        /// Monster 컴포넌트를 가진 히트박스에 맞은 타겟 (중복 방지 HashSet)
        /// </summary>
        public HashSet<Monster> HitTargets { get; private set; } = new();

        public override void Init()
        {
            HitTargets.Clear();
        }

        public override void Release()
        {
            if (pivot is null) return;

            pivot.DOKill();
            pivot.localRotation = Quaternion.identity;
            transform.SetParent(Parent);
        }

        public void Setup(float damage, float lifetime, float angle, float baseRotation, Transform poolParent)
        {
            Damage = damage;
            Lifetime = lifetime;
            Angle = angle;
            BaseRotation = baseRotation;
            Parent = poolParent;

            Swing();
        }

        public void OnHit(Monster monster)
        {
            monster.UnitHp.TakeDamage(Damage);
        }
        
        private void Swing()
        {
            pivot.localRotation = Quaternion.Euler(0, 0, -Angle);

            pivot.localRotation *= Quaternion.Euler(0, 0, BaseRotation);

            pivot.DOLocalRotate(new Vector3(0, 0, BaseRotation + Angle), Lifetime)
                .SetEase(Ease.OutQuad)
                .OnComplete(() => PoolManager.Instance.Despawn(Prefab, this));
        }
    }
}