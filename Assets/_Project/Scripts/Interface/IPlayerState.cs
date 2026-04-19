using TinySurvivors.Enum;

namespace TinySurvivors.Interface
{
    public interface IPlayerState
    {
        public PlayerState State { get; }
        public void Enter();
        public void Exit();
    }
}