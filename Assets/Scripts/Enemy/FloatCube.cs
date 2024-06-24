namespace NCharacter
{
    using System.Collections;
    using System.Collections.Generic;
    using UnityEngine;
    using DG.Tweening;

    public class FloatCube : Enemy
    {
        private float moveDistance = 5f;
        private float floatDistance = 0.5f;
        private float moveDuration = 1.5f;
        private float floatDuration = 0.5f;

        private Sequence moveSequence;
        private Sequence floatSequence;

        void StartMovement()
        {
            if (this == null || transform == null) return;

            moveSequence = DOTween.Sequence();
            moveSequence.Append(transform.DOMoveX(startPosition.x + moveDistance / 2, duration: moveDuration).SetEase(Ease.InOutSine))
                        .Append(transform.DOMoveX(startPosition.x - moveDistance / 2, duration: moveDuration).SetEase(Ease.InOutSine))
                        .SetLoops(-1, LoopType.Yoyo);

            floatSequence = DOTween.Sequence();
            floatSequence.Append(transform.DOMoveY(startPosition.y + floatDistance, duration: floatDuration).SetEase(Ease.InOutSine))
                         .Append(transform.DOMoveY(startPosition.y - floatDistance, duration: floatDuration).SetEase(Ease.InOutSine))
                         .SetLoops(-1, LoopType.Yoyo);

            moveSequence.Play();
            floatSequence.Play();
        }

        protected override void Awake()
        {
            base.Awake();
            enemyName = "FloatCube";
            maxHp = 1;
            hp = 1;
            atk = 1;
            killScore = 1;
        }

        protected override void Start()
        {
            base.Start();
            Invoke(nameof(StartMovement), Random.Range(0.1f, 1.5f));
        }

        protected override void Update()
        {
            base.Update();
        }

        protected override void OnDestroy()
        {
            base.OnDestroy();
            moveSequence?.Kill();
            floatSequence?.Kill();
        }
    }
}
