using UnityEngine;

namespace TinySurvivors.SO.UnitSO
{
    public abstract class UnitData : ScriptableObject
    {
        [Header("유닛 이름")]
        public string baseName;
        [Header("유닛 이미지")] 
        public Sprite baseImage;
        [Header("유닛 설명")] [TextArea(3, 5)]
        public string baseDescription;
        [Header("유닛 기본 체력")]
        public float baseMaxHp;
        [Header("유닛 기본 이동속도")]
        public float baseMoveSpeed;
        [Header("유닛 기본 데미지")] 
        public float baseDamage;
    }
}