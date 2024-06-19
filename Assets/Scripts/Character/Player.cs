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
		private int maxJumpCnt = 2;
		private float speed = 15;
		private float jumpForce = 15f;
		private int jumpCnt = 0;
		private int direction = 1;
		private bool isJumping = false;
		private float defaultScaleX;
		private Rigidbody2D rb;
		private Animator animator;

		public void Jump()
		{
			if (jumpCnt < maxJumpCnt - 1)
			{
				rb.velocity = new Vector2(rb.velocity.x, 0);
				rb.AddForce(Vector2.up * jumpForce, ForceMode2D.Impulse);
				jumpCnt++;
			}
		}

		private void Awake()
		{
			GameManager.instance.SetPlayer(this.gameObject);
			hp = maxHp;
		}

		private void Start()
		{
			rb = this.GetComponent<Rigidbody2D>();
			animator = GetComponent<Animator>();
			defaultScaleX = transform.localScale.x;
		}

		private void Update()
		{
			if (Input.GetKey(KeyCode.D) && !Input.GetKey(KeyCode.A))
			{
				direction = 1;
				animator.SetInteger("PlayerState", 1);
			}
			else if (Input.GetKey(KeyCode.A) && !Input.GetKey(KeyCode.D))
			{
				direction = -1;
				animator.SetInteger("PlayerState", 1);
			}
			else
			{
				direction = 0;
				animator.SetInteger("PlayerState", 0);
			}

			if (Input.GetKeyDown(KeyCode.Space) || Input.GetKeyDown(KeyCode.W))
			{
				Debug.Log("Jump");
				isJumping = true;
			}

			if (direction != 0)
			{
				transform.localScale = new Vector3(direction, 1, 1) * defaultScaleX;
			}
		}

		private void FixedUpdate()
		{
			if (isMovable)
			{
				rb.velocity = new Vector2(speed * direction, rb.velocity.y);

				if (isJumping)
				{
					Jump();
					isJumping = false;
				}
			}



			RaycastHit2D hit = Physics2D.Raycast(transform.position, Vector2.down, 1.5f, LayerMask.GetMask("Ground"));
			if (hit.collider != null)
			{
				jumpCnt = 0;
			}
		}
	}
}
