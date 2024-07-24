namespace NCharacter
{
    using System.Collections;
    using System.Collections.Generic;
    using UnityEngine;
    using DG.Tweening;
    using NManager;

    public class MissileBall : Enemy
    {
        private float speed = 4f;
        private float searchRange = 30.0f;
        private Transform player => GameManager.instance.player.transform;
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
            // ゆっくりと角度をプレイヤーに向ける
            this.transform.up = Vector2.Lerp(this.transform.up, direction, 0.1f);
            this.transform.position = Vector2.Lerp(this.transform.position, this.transform.position + (Vector3)direction * speed, Time.deltaTime);
        }

        protected override void OnDestroy()
        {
            base.OnDestroy();
        }
    }
}
