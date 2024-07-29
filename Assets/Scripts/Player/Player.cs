namespace NCharacter
{
	using System;
	using System.Collections;
	using System.Collections.Generic;
	using UnityEngine;
	using UnityEngine.InputSystem;
	using NManager;

	public class Player : MonoBehaviour
	{
		public bool isInvincible = false;
		public bool isShield = false;
		public bool isThunder { get; private set; } = false;
		public int maxHp { get; private set; } = 3;
		public int hp { get; private set; }
		public int atk { get; private set; } = 1;
		public bool isMovable = true;
		public bool isOnGame = true;
		public float defaultScaleX { get; private set; }
		private int maxJumpCnt = 2;
		private float speed = 15;
		private float jumpForce = 17.5f;
		[SerializeField]
		private int jumpCnt = 0;
		private int direction = 1;
		private float hitInterval = 0.5f;
		private bool isJumping = false;
		private float thunderIntensity = 1;
		private Rigidbody2D rb => this.GetComponent<Rigidbody2D>();
		private Animator animator => this.GetComponent<Animator>();

		private InputAction moveRight, moveLeft, jump;
		private PlayerInput playerInput => this.GetComponent<PlayerInput>();

		public void Heal(int amount)
		{
			hp += amount;
			if (hp > maxHp)
			{
				hp = maxHp;
			}
		}

		public void CutHp(int damage)
		{
			if (isInvincible) return;

			this.GetComponent<PlayerParticles>().ChangeColorToRed();
			hp -= damage;
			if (hp <= 0)
			{
				hp = 0;
				GameManager.instance.GameOver();
				animator.SetInteger("PlayerState", 0);
			}
		}

		public void AddForce(Vector2 force)
		{
			rb.velocity = Vector2.zero;
			rb.AddForce(force, ForceMode2D.Impulse);
		}

		public void JumpByEnemy(float f)
		{
			rb.velocity = new Vector2(rb.velocity.x, 0);
			rb.AddForce(Vector2.up * jumpForce * f, ForceMode2D.Impulse);
			jumpCnt = 0;
		}

		public void SetIsThunder(bool isThunder, float intensity = 1)
		{
			this.isThunder = isThunder;
			this.thunderIntensity = intensity;
		}

		private void _Jump()
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
			defaultScaleX = transform.localScale.x;
			isOnGame = true;


			moveRight = playerInput.actions["MoveRight"];
			moveLeft = playerInput.actions["MoveLeft"];
			jump = playerInput.actions["Jump"];
		}

		private void Update()
		{
			Debug.Log(moveRight.ReadValue<float>());
			if (isMovable && isOnGame)
			{
				if (moveRight.ReadValue<float>() > 0 && !(moveLeft.ReadValue<float>() > 0))
				{
					direction = 1;
					animator.SetInteger("PlayerState", 0);
				}
				else if (!(moveRight.ReadValue<float>() > 0) && (moveLeft.ReadValue<float>() > 0))
				{
					direction = -1;
					animator.SetInteger("PlayerState", 1);
				}
				else
				{
					direction = 0;
					animator.SetInteger("PlayerState", 0);
				}

				if (jump.WasPressedThisFrame())
				{
					isJumping = true;
				}

				if (rb.velocity.x < -0.01f)
				{
					transform.localScale = new Vector3(-1, 1, 1) * defaultScaleX;
				}
				else if (rb.velocity.x > 0.01f)
				{
					transform.localScale = new Vector3(1, 1, 1) * defaultScaleX;
				}

				GameManager.instance.SetMaxAltitude(transform.position.y);
			}
		}

		private void FixedUpdate()
		{
			if (isMovable && isOnGame)
			{
				rb.velocity = new Vector2(speed * direction, rb.velocity.y);
				if (isJumping)
				{
					_Jump();
					isJumping = false;
				}
			}

			if (isOnGame)
			{
				rb.constraints = RigidbodyConstraints2D.FreezeRotation;
			}
			else
			{
				rb.constraints = RigidbodyConstraints2D.FreezeAll;
			}

			RaycastHit2D hit1 = Physics2D.Raycast(transform.position + new Vector3(0.7f, 0, 0), Vector2.down, 2f, LayerMask.GetMask("Ground"));
			RaycastHit2D hit2 = Physics2D.Raycast(transform.position + new Vector3(-1.0f, 0, 0), Vector2.down, 2f, LayerMask.GetMask("Ground"));
			if (hit1.collider != null || hit2.collider != null)
			{
				jumpCnt = 0;
			}
		}

		//TODO デリゲートで動作を変えられるようにする
		protected virtual void OnTriggerEnter2D(Collider2D other)
		{
			Enemy enemy = other.GetComponent<Enemy>();
			if (enemy != null && isMovable && isOnGame)
			{
				if (isThunder)
				{
					JumpByEnemy(2.5f * thunderIntensity);
					enemy.CutHp((int)(this.atk * thunderIntensity));
					this.GetComponent<PlayerParticles>().PlayJumpSe();
				}
				else if (isShield)
				{
					if (isMovable)
					{
						Camera.main.GetComponent<CameraMoveScript>().ShakeCamera();
						if (other.transform.position.y < this.transform.position.y)
						{
							JumpByEnemy(1.5f);
							enemy.CutHp(this.atk);
							this.GetComponent<PlayerParticles>().PlayJumpSe();
						}
						else
						{

							Vector3 dir = (this.transform.position - enemy.transform.position).normalized;
							this.AddForce(new Vector2(dir.x, dir.y) * 30);
							SoundManager.instance.PlaySe("shield");
						}
					}
				}
				else
				{
					if (isMovable)
					{
						Camera.main.GetComponent<CameraMoveScript>().ShakeCamera();
						if (other.transform.position.y < this.transform.position.y)
						{
							JumpByEnemy(1.5f);
							enemy.CutHp(this.atk);
							this.GetComponent<PlayerParticles>().PlayJumpSe();
						}
						else
						{
							if (!isInvincible)
							{
								this.CutHp(enemy.atk);
								Vector3 dir = (this.transform.position - enemy.transform.position).normalized;
								this.AddForce(new Vector2(dir.x, dir.y) * 30);
								this.GetComponent<PlayerParticles>().PlayDamageSe();
								// 無敵時間
								StartCoroutine(InvincibilityCoroutine());
							}
						}
					}
				}
			}
		}

		private IEnumerator InvincibilityCoroutine()
		{
			isInvincible = true;
			yield return new WaitForSeconds(hitInterval);
			isInvincible = false;
		}
	}
}
