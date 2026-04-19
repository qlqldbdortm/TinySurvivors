using UnityEngine;

namespace TinySurvivors.Unit.Monster
{
    [RequireComponent(typeof(CapsuleCollider2D))]
    public class MonsterColliderHandler : MonoBehaviour
    {
        private Monster BaseMonster { get; set; }
        private CapsuleCollider2D Collider { get; set; }
        
        private void Awake()
        {
            Collider = GetComponent<CapsuleCollider2D>();
            Collider.size = new Vector2(0.5f, 0.75f);
            Collider.offset = new Vector2(0, 0.15f);
        }
        
        public void SetColliderOn()
        {
            Collider.enabled = true;
        }

        public void SetColliderOff()
        {
            Collider.enabled = false;
        }
        
        public void Init(Monster monster)
        {
            BaseMonster = monster;
        }
        
        # region ◆ Collision Enter, Stay, Exit ◆ 
        private void OnCollisionEnter2D(Collision2D other)
        {
            if (other.transform.TryGetComponent(out Player.Player player))
            {
                // TODO : 지금은 기본적인 데미지만 주고 있는데 나중에 시간이 지날수로 난이도 배율 추가
                player.UnitHp.TakeDamage(BaseMonster.MonsterStat.Damage.Value);
            }
        }
        
        private void OnCollisionStay2D(Collision2D other)
        {
            if (!other.transform.TryGetComponent(out Player.Player player)) return;
            if (!player.DamageController.CanTakeDamage()) return;
            
            player.DamageController.OnDamaged();
            player.UnitHp.TakeDamage(BaseMonster.MonsterStat.Damage.Value);
        }
        
        private void OnCollisionExit2D(Collision2D other)
        {
            
        }
        #endregion
        
        # region ◆ Trigger Enter, Stay, Exit ◆ 
        private void OnTriggerEnter2D(Collider2D other)
        {

        }

        private void OnTriggerStay2D(Collider2D other)
        {
            
        }

        private void OnTriggerExit2D(Collider2D other)
        {
            
        }
        #endregion
    }
}