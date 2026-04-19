using TinySurvivors.SO.UnitSO.PlayerSO;

namespace TinySurvivors.Unit.Player
{
    public class PlayerStat : BaseStat
    {
        public UnitStat RecoveryHp;
        public UnitStat Critical;
        public UnitStat CriticalDamage;
        public UnitStat Luck;
        public UnitStat Greed;
        public UnitStat Growth;
        public UnitStat MagnetRange;
        public float InvincibleTime;
        
        public PlayerStat(PlayerData data)
        {
            MaxHp = new UnitStat(data.baseMaxHp);
            MoveSpeed = new UnitStat(data.baseMoveSpeed);
            Damage = new UnitStat(data.baseDamage);
            RecoveryHp = new UnitStat(data.baseRecoveryHp);
            Critical = new UnitStat(data.baseCritical);
            CriticalDamage = new UnitStat(data.baseCriticalDamage);
            Luck = new UnitStat(data.baseLuck);
            Greed = new UnitStat(data.baseGreedRatio);
            Growth = new UnitStat(data.baseGrowthRatio);
            MagnetRange = new UnitStat(data.baseMagnetRange);
            InvincibleTime = data.baseInvincibleTime;
        }
    }
}