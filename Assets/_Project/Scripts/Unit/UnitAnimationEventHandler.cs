using UnityEngine;

namespace TinySurvivors.Unit
{
    public abstract class UnitAnimationEventHandler : MonoBehaviour
    {
        public abstract void OnAttackStart();
        /// <summary>
        /// 애니메이션 중 투사체를 발사하는 이벤트
        /// </summary>
        public abstract void OnShootProj();

        /// <summary>
        /// 공격 애니메이션이 끝났음을 알리는 이벤트
        /// </summary>
        public abstract void OnAttackEnd();

        public abstract void OnDeath();
    }
}