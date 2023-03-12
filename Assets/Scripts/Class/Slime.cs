using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Slime : Enemy
{
    void attack()
    {
    }

    public float stopTime = 1f; // 停止時間

    private float moveTime = 1f; // 移動時間

    private float stopTimer; // 停止時間のタイマー

    private bool isMoving = true; // 移動中かどうか

    public override void Start()
    {
        base.Start();
        name = "Slime";
        hp = 1;
        atk = 1;
        killScore = 1;
    }

    public override void Update()
    {
        base.Update();
        if (isMoving)
        {
            // 移動中の処理
            transform.Translate(new Vector2(direction * Time.deltaTime, 0));

            // 移動時間が経過したら停止する
            moveTime -= Time.deltaTime;
            if (moveTime <= 0)
            {
                isMoving = false;
                stopTimer = Random.Range(1, 3);
            }
        }
        else
        {
            // 停止中の処理
            stopTimer -= Time.deltaTime;

            // 停止時間が経過したら移動する
            if (stopTimer <= 0)
            {
                isMoving = true;
                moveTime = Random.Range(1, 3);
                direction = Random.Range(0, 2) == 0 ? -1 : 1; // 左右どちらに移動するかランダムに決定
            }
        }
    }
}
