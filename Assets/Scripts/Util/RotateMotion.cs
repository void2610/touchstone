namespace NUtil
{
    using UnityEngine;
    using DG.Tweening;

    public class RotateMotion : MonoBehaviour
    {
        private Sequence rotateSequence;

        void Start()
        {
            rotateSequence = DOTween.Sequence();
            rotateSequence.Append(transform.DORotate(new Vector3(180, 180, 180), duration: 3f).SetEase(Ease.Linear))
                          .SetLoops(-1, LoopType.Incremental);
            rotateSequence.Play();
        }

        void OnDestroy()
        {
            rotateSequence?.Kill();
        }
    }
}
