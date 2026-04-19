using UnityEngine;

namespace TinySurvivors.Unit.Monster
{
    [RequireComponent(typeof(Rigidbody2D))]
    public class MonsterController : MonoBehaviour
    {
        private Monster BaseMonster { get; set; }
        private Rigidbody2D rb;

        public bool IsRight { get; private set; }
        public bool IsAttacking { get; set; }

        private void Awake()
        {
            rb = GetComponent<Rigidbody2D>();
            rb.gravityScale = 0;
            rb.freezeRotation = true;
        }

        public void Init(Monster monster)
        {
            BaseMonster = monster;
        }

        public void MoveTo(Vector2 targetPos)
        {
            if (IsAttacking) return;
            Vector2 dir = (targetPos - rb.position).normalized;
            rb.MovePosition(rb.position + dir * (BaseMonster.MonsterStat.MoveSpeed.Value * Time.fixedDeltaTime));
            IsTargetRight(dir);
        }

        /// <summary>
        /// 공격받은 방향으로부터 power만큼 밀려나게 하는 메서드
        /// </summary>
        public void KnockBack(Vector2 targetPos, float power)
        {
            rb.velocity = Vector2.zero;
            Vector2 dir = (rb.position - targetPos).normalized;
            rb.AddForce(dir * power, ForceMode2D.Impulse);
        }

        private void IsTargetRight(Vector2 dir)
        {
            IsRight = dir.x > 0;
        }

        /// <summary>
        /// 몬스터가 풀에서 나오면 rigidbody를 sleep 모드에서 깨우는 메서드
        /// </summary>
        public void SetRigidBodyOn()
        {
            rb.WakeUp();
        }

        /// <summary>
        /// 몬스터가 죽으면 rigidBody를 sleep 모드로 변경하는 메서드
        /// </summary>
        public void SetRigidBodyOff()
        {
            rb.Sleep();
        }

        #region ◆ 보스 몬스터 돌진 공격 관련 ◆

        private Vector2 dashDir;

        public void StartDash(Vector2 dir, float dashSpeed)
        {
            dashDir = dir.normalized;
            rb.velocity = dashDir * dashSpeed;

            IsTargetRight(dashDir);

            var bt = BaseMonster.transform;
            bt.localScale = IsRight
                ? new Vector3(-1, 1, 1) * bt.localScale.z
                : new Vector3(1, 1, 1) * bt.localScale.z; 
        }

        public void EndDash()
        {
            rb.velocity = Vector2.zero;
        }

        #endregion
    }
}