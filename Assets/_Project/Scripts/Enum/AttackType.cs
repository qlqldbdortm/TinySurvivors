namespace TinySurvivors.Enum
{
    public enum AttackType
    {
        None,
        /// <summary>
        /// 근접 공격
        /// </summary>
        Melee = 1,
        /// <summary>
        /// 가장 가까운 적이 있는 방향으로 공격
        /// </summary>
        Range = 2,
        /// <summary>
        /// Unit을 중심으로 회전하는 공격
        /// </summary>
        Orbit = 3,
        /// <summary>
        /// 가장 가까운 적을 타겟으로 하는 공격
        /// </summary>
        Target = 4,
        /// <summary>
        /// 플레이어를 중심으로 360도 방향으로 랜덤하게 하는 공격
        /// </summary>
        Random = 5,
        /// <summary>
        /// 플레이어를 기준으로 랜덤한 위치에 생성되는 장판형 공격
        /// </summary>
        Area = 6,
    }
}