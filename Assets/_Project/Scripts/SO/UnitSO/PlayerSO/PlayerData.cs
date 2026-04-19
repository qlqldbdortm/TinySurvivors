using TinySurvivors.SO.WeaponSO;
using UnityEngine;

namespace TinySurvivors.SO.UnitSO.PlayerSO
{
    [CreateAssetMenu(fileName = "PlayerData", menuName = "ScriptableObjects/Unit/01.PlayerData", order = 1)]
    public class PlayerData : UnitData
    {
        [Header("플레이어 기본 체력 회복")]
        public float baseRecoveryHp;
        [Header("플레이어 기본 행운")] [Tooltip("아이템 드롭률 증가")] 
        public float baseLuck;
        [Header("플레이어 기본 크리티컬 확률")]
        public float baseCritical;
        [Header("플레이어 기본 크리티컬 데미지")] 
        public float baseCriticalDamage;
        [Header("플레이어 기본 골드 획득량")] 
        public float baseGreedRatio;
        [Header("플레이어 기본 경험치 획득량")]
        public float baseGrowthRatio;
        [Header("플레이어 기본 경험치 & 아이템 획득 범위")] 
        public float baseMagnetRange;
        [Header("플레이어 기본 피격 무적시간")]
        public float baseInvincibleTime;

        [Header("플레이어 선택 시 기본 무기")]
        public WeaponData baseWeaponData;
    }
}