using DG.Tweening;
using UnityEngine;

namespace TinySurvivors.Unit.Monster
{
    public class MonsterSpriteController : MonoBehaviour
    {
        private SpriteRenderer[] spriteRenderers;

        private void Awake()
        {
            spriteRenderers = GetComponentsInChildren<SpriteRenderer>();
        }
        
        /// <summary>
        /// 피격 시 스프라이트를 깜빡이게 하는 메서드
        /// </summary>
        public void StartBlink()
        {
            StopBlink();

            foreach (var sr in spriteRenderers)
            {
                if (sr.transform.name is "Shadow") continue;
                sr.DOFade(0.1f,0.1f)
                    .SetLoops(-1, LoopType.Yoyo)
                    .SetEase(Ease.Linear);
            }
        }
        
        /// <summary>
        /// 피격 상태에서 넘어갔을 때 초기화 하는 메서드
        /// </summary>
        public void StopBlink()
        {
            foreach (var sr in spriteRenderers)
            {
                if (sr.transform.name is "Shadow") continue;
                sr.DOKill();
                var color = sr.color;
                color.a = 1;
                sr.color = color;
            }
        }

        /// <summary>
        /// 죽었을 때 스프라이트를 약간 어둡게 변경하는 메서드
        /// </summary>
        public void DarkSprite()
        {
            foreach (var sr in spriteRenderers)
            {
                if (sr.transform.name is "Shadow") continue;
                sr.DOColor(new Color(0.3f, 0.3f, 0.3f, 1f),0f);
            }
        }

        public void ResetSprite()
        {
            foreach (var sr in spriteRenderers)
            {
                if (sr.transform.name is "Shadow") continue;
                sr.DOColor(new Color(1f, 1f, 1f, 1f), 0f);
            }
        }
    }
}