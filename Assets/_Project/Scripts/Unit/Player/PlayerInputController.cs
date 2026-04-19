using TinySurvivors.Enum;
using UnityEngine;

namespace TinySurvivors.Unit.Player
{
    [RequireComponent(typeof(Rigidbody2D))]
    public class PlayerInputController : MonoBehaviour
    {
        [SerializeField] private Transform lookTransform;
        private Player BasePlayer { get; set; }
        public Vector2 MoveDir { get; private set; }

        private Rigidbody2D rb;
        private bool canMove = true;
        private bool isRight;

        private void Awake()
        {
            rb = GetComponent<Rigidbody2D>();
            rb.gravityScale = 0;
            rb.freezeRotation = true;
        }
        
        private void FixedUpdate()
        {
            if (!canMove)
            {
                rb.velocity = Vector2.zero;
                return;
            }

            rb.velocity = MoveDir * BasePlayer.PlayerStat.MoveSpeed.Value;
            lookTransform.localScale = isRight ? new Vector3(-1, 1, 1) : new Vector3(1, 1, 1);
        }
        
        public void Init(Player player)
        {
            BasePlayer = player;
        }
        
        public void OnMove(Vector2 dir)
        {
            MoveDir = dir;
            BasePlayer.StateMachine.ChangeState(dir == Vector2.zero ? PlayerState.Idle : PlayerState.Move);
            
            if (dir.x != 0)
            {
                isRight = dir.x > 0;
            }
        }

        public void SetMoveEnabled(bool moveEnable)
        {
            canMove = moveEnable;
            
            if (!enabled)
            {
                MoveDir = Vector2.zero;
                rb.velocity = Vector2.zero;
            }
        }
    }
}