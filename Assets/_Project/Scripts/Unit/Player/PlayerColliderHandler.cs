using UnityEngine;

namespace TinySurvivors.Unit.Player
{
    [RequireComponent(typeof(CapsuleCollider2D))]
    public class PlayerColliderHandler : MonoBehaviour
    {
        public Player BasePlayer { get; private set; }

        public void Init(Player player)
        {
            BasePlayer = player;
        }        
        
        # region ◆ Collision Enter, Stay, Exit ◆ 
        private void OnCollisionEnter2D(Collision2D other)
        {

        }
        
        private void OnCollisionStay2D(Collision2D other)
        {
        
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