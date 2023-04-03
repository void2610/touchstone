namespace NCharacter
{
	using System;
	using System.Collections;
	using System.Collections.Generic;
	using NControl;
	using UnityEngine;

	public class Player : Character
	{
		public float speed;

		public bool jumpable = true;

		public float jumpForce = 600f;

		public int jp = 0;

		private Rigidbody2D rb;

		private Vector2 speedLimit = new Vector2(5, 30);

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
			if (Input.GetKeyDown(KeyCode.Space) || Input.GetKeyDown(KeyCode.W))
			{
				if (this.jp < 1)
				{
					this.rb.AddForce(transform.up * jumpForce);
					jrs.jumpCount++;
				}
			}
		}

		public override void FixedUpdate()
		{
			base.FixedUpdate();

			if (GameObject.Find("GameController").GetComponent<GameControlScript>().movable)
			{
				//右入力で右向きに動く
				if (Input.GetKey(KeyCode.D) && !Input.GetKey(KeyCode.A))
				{
					direction = 1;
					if (Math.Abs(rb.velocity.x) < 100000)
					{
						rb.velocity = new Vector2(speed * direction, rb.velocity.y);
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
						rb.velocity = new Vector2(speed * direction, rb.velocity.y);
					}
				}
				else
				{
					if (this.jp < 1)
					{
						animator.SetInteger("PlayerState", 0);
					}
				}

				if (Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.D))
				{
					if (this.jp < 1)
					{
						animator.SetInteger("PlayerState", 1);
					}
				}
			}
		}
	}
}
