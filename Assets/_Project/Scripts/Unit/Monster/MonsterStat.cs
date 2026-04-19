using TinySurvivors.Enum;
using TinySurvivors.SO.UnitSO.MonsterSO;

namespace TinySurvivors.Unit.Monster
{
    public class MonsterStat : BaseStat
    {
        public MonsterData Data { get; private set; }
        public AttackType AttackType; 
        public UnitStat DetectRange;
        public UnitStat AttackSpeed;
        
        public MonsterStat(MonsterData data)
        {
            Data = data;
            
            MaxHp = new UnitStat(Data.baseMaxHp);
            MoveSpeed = new UnitStat(Data.baseMoveSpeed);
            Damage = new UnitStat(Data.baseDamage);
            
            AttackType = Data.baseAttackType;
            DetectRange = new UnitStat(Data.baseDetectRange);
            AttackSpeed = new UnitStat(Data.baseAttackSpeed);
        }
    }
}