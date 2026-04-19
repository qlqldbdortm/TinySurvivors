using System.Collections.Generic;
using System.Threading;
using TinySurvivors.Unit.Monster;
using UnityEngine;

namespace TinySurvivors.Projectile.Hitbox
{
    public class AreaHitbox : BaseHitbox
    {
        public DotAreaProjectile dotArea;

        private readonly HashSet<Monster> areaTargets = new();

        /// <summary>
        /// 몬스터가 영역안에 들어왔을 떄 HashSet에 포함시킴.
        /// </summary>
        public override void OnTriggerEnter2D(Collider2D collision)
        {
            if (!collision.CompareTag("Monster") || !collision.TryGetComponent<Monster>(out var monster)) return;

            areaTargets.Add(monster);
        }

        /// <summary>
        /// 몬스터가 영역 밖으로 나갔을 때 hashSet에서 제거시킴.
        /// </summary>
        private void OnTriggerExit2D(Collider2D collision)
        {
            if (!collision.CompareTag("Monster") || !collision.TryGetComponent<Monster>(out var monster)) return;

            areaTargets.Remove(monster);
        }

        public IReadOnlyCollection<Monster> AreaTargets => areaTargets;
    }
}