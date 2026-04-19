using System;

namespace TinySurvivors.SO.WeaponSO
{
    [Serializable]
    public class WeaponLevelData
    {
        /// <summary>
        /// 무기 레벨업 시 증가하는 데미지 (곱연산) 
        /// </summary>
        public float damageMultiplier;
        /// <summary>
        /// 무기 레벨업 시 감소하는 공격속도 (곱연산) 
        /// </summary>
        public float intervalMultiplier;
        /// <summary>
        /// 무기 레벨업 시 증가하는 발사체 수 (합연산)
        /// </summary>
        public int additionalProjectile;
    }
}