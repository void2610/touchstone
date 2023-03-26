namespace NCharacter
{
    using System.Collections;
    using System.Collections.Generic;
    using UnityEngine;

    public class Enemy : Character
    {
        //攻撃処理(この中でCutHPを呼ぶ)(キャラによって違う)
        public virtual void Attack()
        {
        }

        public override void Start()
        {
            base.Start();
            name = "Enemy";
            hp = 1;
            atk = 1;
            killScore = 1;
        }

        public override void Update()
        {
            base.Update();
            if (hp <= 0)
            {
                Destroy(this.gameObject);
            }
        }

        void OnCollisionEnter2D(Collision2D other) //敵に触れた時の処理
        {
            Character target = null;
            if (other.gameObject.tag == "PlayerTrigger")
            {
                target = SearchCharacter(other.gameObject);
            }
            if (target != null)
            {
                CutHP (target);
            }
        }
    }
}
