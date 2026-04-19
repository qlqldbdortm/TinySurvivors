using TinySurvivors.Enum;
using TinySurvivors.Projectile;
using TinySurvivors.SO.AttackPatternSO;
using Unity.Collections;
using UnityEngine;

namespace TinySurvivors.SO.WeaponSO
{
    [CreateAssetMenu(fileName = "WeaponData", menuName = "ScriptableObjects/Weapon/01.WeaponData", order = 1)]
    public class WeaponData : ScriptableObject
    {
        public string weaponName;
        public Sprite weaponIcon;
        
        [TextArea(0, 3)] 
        public string weaponDescription;
        
        [Header("무기 기본 정보")] 
        public float baseWeaponDamage;
        public float baseAttackInterval;
        public float baseAttackRange;
        public int baseProjectileCount;
        public float baseProjectileSpeed;
        public BaseProjectile baseProjectilePrefab;

        [Header("공격 방식 & 데이터")] 
        public AttackType baseAttackType;
        public AttackPatternData baseAttackPatternData;

        [Header("Pool에 미리 생성해둘 수")] 
        public int capacity;

        [Header("무기 최대 레벨")]
        public int maxLevel = 6;
        
        [Header("무기 레벨 당 변동 수치")] 
        public WeaponLevelData[] weaponLevelData;
    }
}