namespace NCharacter
{
    using System.Collections;
    using System.Collections.Generic;
    using UnityEngine;
    using DG.Tweening;
    using NManager;

    public class BlessHolder : Enemy
    {
        private float speed = 5f;
        private float searchRange = 30.0f;
        private Transform player => GameManager.instance.player.transform;

        private float leftLimit = -15.0f;
        private float rightLimit = 15.0f;
        protected override void Awake()
        {
            base.Awake();
            enemyName = "MissileBall";
            maxHp = 1;
            hp = 1;
            atk = 1;
            killScore = 1;
        }

        protected override void Start()
        {
            base.Start();
        }

        protected override void Update()
        {
            base.Update();
            if (Vector3.Distance(this.transform.position, player.position) > searchRange) return;

            Vector2 direction = (player.position - this.transform.position).normalized;
            Vector3 newPosition = Vector2.Lerp(this.transform.position, this.transform.position - (Vector3)direction * speed, Time.deltaTime);

            // 左右の制限を追加
            newPosition.x = Mathf.Clamp(newPosition.x, leftLimit, rightLimit);

            this.transform.position = newPosition;
        }

        protected override void DestroyByPlayer()
        {
            base.DestroyByPlayer();
            GameManager.instance.GetComponent<BlessManager>().GetRandomBless(this.transform.position);
        }
    }
}
