using TinySurvivors.Enum;
using TinySurvivors.Unit.Monster.Drop;
using UnityEngine;

namespace TinySurvivors.SO.UnitSO.MonsterSO
{
    [CreateAssetMenu(fileName = "MonsterData", menuName = "ScriptableObjects/Unit/02.MonsterData", order = 2)]
    public class MonsterData : UnitData
    {
        [Header("몬스터의 공격 타입 (근거리, 원거리)")] 
        public AttackType baseAttackType;
        [Header("몬스터의 플레이어 탐지 범위")] 
        public float baseDetectRange;
        [Header("몬스터의 공격속도(원거리만)")] 
        public float baseAttackSpeed;
        
        [Header("몬스터 드롭")]
        public Exp smallExp;
        public Exp largeExp;
    }
}