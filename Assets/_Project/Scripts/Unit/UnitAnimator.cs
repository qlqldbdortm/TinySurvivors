using TinySurvivors.Enum;
using UnityEngine;

namespace TinySurvivors.Unit
{
    public class UnitAnimator : MonoBehaviour
    {
        private static readonly int State = Animator.StringToHash("State");
        public Animator Animator { get; private set; }

        private void Awake()
        {
            Animator ??= GetComponent<Animator>();
        }

        public void SetState(PlayerState state)
        {
            Animator.SetInteger(State, (int)state);
        }

        public void SetState(MonsterState state)
        {
            // MonsterAttackState를 반복하면 ATTACK_BOW 애니메이션이 초기화가 안되서 초기화 하는 단계 추가.
            if (state == MonsterState.Attack)
            {
                Animator.Play("ATTACK_BOW", 0, 0f);
            }
            else if (state is MonsterState.BossBullet)
            {
                Animator.Play("BOSS_BULLET", 0, 0f);
            }

            Animator.SetInteger(State, (int)state);
        }
    }
}