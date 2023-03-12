using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : Character
{
    //ビルド時は270に
    public float speed;

    public bool jumpable = true;

    public float jumpForce = 500f;

    public int jp = 0;

    private Rigidbody2D rb;

    Animator animator;

    JumpResetScript jrs;

    public override void Start()
    {
        base.Start();
        name = "Player";
        hp = 10;
        atk = 1;

        rb = this.GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        jrs = GameObject.Find("Leg").GetComponent<JumpResetScript>();
        direction = 1;
    }

    public override void Update()
    {
        base.Update();

        jp = jrs.jumpCount;
        if (
            GameObject
                .Find("GameController")
                .GetComponent<GameControlScript>()
                .movable
        )
        {
            //右入力で右向きに動く
            if (Input.GetKey(KeyCode.D) && !Input.GetKey(KeyCode.A))
            {
                direction = 1;
                if (Math.Abs(rb.velocity.x) < 10)
                {
                    rb.AddForce(transform.right * speed);
                }
            }
            else
            {
                if (this.jp < 1)
                {
                    animator.SetInteger("PlayerState", 0);
                }
            }

            //左入力で左向きに動く
            if (Input.GetKey(KeyCode.A) && !Input.GetKey(KeyCode.D))
            {
                direction = -1;
                if (Math.Abs(rb.velocity.x) < 100)
                {
                    rb.AddForce(-transform.right * speed);
                }
            }
            else
            {
                if (this.jp < 1)
                {
                    animator.SetInteger("PlayerState", 0);
                }
            }

            //ジャンプ
            if (Input.GetKeyDown(KeyCode.Space) || Input.GetKeyDown(KeyCode.W))
            {
                if (this.jp < 1)
                {
                    this.rb.AddForce(transform.up * jumpForce);
                    jrs.jumpCount++;
                }
            }
            if (Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.D))
            {
                if (this.jp < 1)
                {
                    animator.SetInteger("PlayerState", 1);
                }
            }
            if (!Input.GetKey(KeyCode.A) || !Input.GetKey(KeyCode.D))
            {
                this.rb.velocity = new Vector2(0, this.rb.velocity.y);
            }
        }

        if (Math.Abs(rb.velocity.x) > 30)
        {
            if (rb.velocity.x > 0)
            {
                rb.velocity = new Vector2(30, rb.velocity.y);
            }
            else
            {
                rb.velocity = new Vector2(-30, rb.velocity.y);
            }
        }
        if (Math.Abs(rb.velocity.y) > 30)
        {
            if (rb.velocity.y > 0)
            {
                rb.velocity = new Vector2(rb.velocity.x, 10);
            }
            else
            {
                rb.velocity = new Vector2(rb.velocity.x, -10);
            }
        }
    }
}
