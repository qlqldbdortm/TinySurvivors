using TinySurvivors.Enum;

namespace TinySurvivors.Interface
{
    public interface IMonsterState
    {
        public MonsterState State { get; }
        public void Enter();
        public void Update();
        public void Exit();
    }
}