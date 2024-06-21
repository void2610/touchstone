namespace NCharacter
{
    using System.Collections;
    using System.Collections.Generic;
    using UnityEngine;
    using DG.Tweening;

    public class Bat : Enemy
    {
        void attack()
        {
        }
        private float moveDistance = 5f;  // 左右に移動する距離
        private float floatDistance = 0.5f;  // 上下に動く距離
        private float moveDuration = 1.5f;  // 左右に移動する時間
        private float floatDuration = 0.5f;  // 上下に動く時間

        private Sequence moveSequence;
        private Sequence floatSequence;

        void StartMovement()
        {
            if (this == null || transform == null) return;

            moveSequence = DOTween.Sequence();
            moveSequence.Append(transform.DOMoveX(startPosition.x + moveDistance / 2, duration: moveDuration).SetEase(Ease.InOutSine))
                        .Append(transform.DOMoveX(startPosition.x - moveDistance / 2, duration: moveDuration).SetEase(Ease.InOutSine))
                        .SetLoops(-1, LoopType.Yoyo);

            // 上下に動くシーケンスを作成
            floatSequence = DOTween.Sequence();
            floatSequence.Append(transform.DOMoveY(startPosition.y + floatDistance, duration: floatDuration).SetEase(Ease.InOutSine))
                         .Append(transform.DOMoveY(startPosition.y - floatDistance, duration: floatDuration).SetEase(Ease.InOutSine))
                         .SetLoops(-1, LoopType.Yoyo);

            // シーケンスを再生
            moveSequence.Play();
            floatSequence.Play();
        }

        protected override void Awake()
        {
            base.Awake();
            enemyName = "Bat";
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
