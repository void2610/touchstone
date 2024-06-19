namespace NCharacter
{
	using System;
	using System.Collections;
	using System.Collections.Generic;
	using NManager;
	using UnityEngine;

	public class Player : MonoBehaviour
	{
		public bool isInvincible { get; set; } = false;
		public int maxHp { get; private set; } = 10;
		public int hp { get; private set; }
		public bool isMovable { get; set; } = true;
		private float speed;
		private float jumpForce = 12f;
		private int jumpCnt = 0;
		private int direction;
		private Rigidbody2D rb;
		private Vector2 speedLimit = new Vector2(5, 30);
		private Animator animator;

		private void Awake()
		{
			GameManager.instance.SetPlayer(this.gameObject);
			direction = 1;
			hp = maxHp;
			speed = 15;
		}

		private void Start()
		{
			rb = this.GetComponent<Rigidbody2D>();
			animator = GetComponent<Animator>();
		}

		private void Update()
		{
			if (Input.GetKeyDown(KeyCode.Space) || Input.GetKeyDown(KeyCode.W))
			{
				if (jumpCnt < 1)
				{
					rb.velocity = new Vector2(rb.velocity.x, 0);
					rb.AddForce(transform.up * jumpForce, ForceMode2D.Impulse);
					jumpCnt++;
				}
			}

			RaycastHit2D hit = Physics2D.Raycast(transform.position, Vector2.down, 1.5f, LayerMask.GetMask("Ground"));
			if (hit.collider != null)
			{
				jumpCnt = 0;
			}
		}

		private void FixedUpdate()
		{
			if (isMovable)
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
					if (jumpCnt < 1)
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
					if (this.jumpCnt < 1)
					{
						animator.SetInteger("PlayerState", 0);
					}
				}
				if (!Input.GetKey(KeyCode.D) && !Input.GetKey(KeyCode.A))
				{
					rb.velocity = new Vector2(0, rb.velocity.y);
				}

				if (MathF.Abs(rb.velocity.x) > 0.1f)
				{
					if (jumpCnt < 1)
					{
						animator.SetInteger("PlayerState", 1);
					}
				}
			}
		}
	}
}
