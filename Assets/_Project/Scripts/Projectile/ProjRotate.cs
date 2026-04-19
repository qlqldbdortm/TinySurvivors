using DG.Tweening;
using UnityEngine;

namespace TinySurvivors.Projectile
{
    public class ProjRotate : MonoBehaviour
    {
        public void StartRotate()
        {
            transform.DOKill();

            transform.DORotate(new Vector3(0, 0, 360f), 0.5f, RotateMode.FastBeyond360)
                .SetLoops(-1, LoopType.Incremental)
                .SetEase(Ease.Linear);
        }

        public void StopRotate()
        {
            transform.DOKill();
        }
    }
}