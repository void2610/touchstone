namespace NCharacter
{
	using System;
	using System.Collections;
	using System.Collections.Generic;
	using NManager;
	using UnityEngine;

	public class Player : MonoBehaviour
	{
		public bool isInvincible = false;
		public bool isShield = false;
		public bool isThunder { get; private set; } = false;
		public int maxHp { get; private set; } = 3;
		public int hp { get; private set; }
		public int atk { get; private set; } = 1;
		public bool isMovable { get; set; } = true;
		public float defaultScaleX { get; private set; }
		private int maxJumpCnt = 2;
		private float speed = 15;
		private float jumpForce = 17.5f;
		private int jumpCnt = 0;
		private int direction = 1;
		private float hitInterval = 0.5f;
		private bool isJumping = false;
		private float maxAltitude = 0;
		private float thunderIntensity = 1;
		private Rigidbody2D rb => this.GetComponent<Rigidbody2D>();
		private Animator animator => this.GetComponent<Animator>();

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
		}

		private void Update()
		{
			if (isMovable)
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

				if (transform.position.y > maxAltitude)
				{
					maxAltitude = transform.position.y;
					GameManager.instance.SetMaxAltitude(maxAltitude);
				}
			}
		}

		private void FixedUpdate()
		{
			if (isMovable)
			{
				rb.velocity = new Vector2(speed * direction, rb.velocity.y);

				if (isJumping)
				{
					_Jump();
					isJumping = false;
				}
			}

			RaycastHit2D hit = Physics2D.Raycast(transform.position, Vector2.down, 2f, LayerMask.GetMask("Ground"));
			if (hit.collider != null)
			{
				jumpCnt = 0;
			}
		}

		//TODO デリゲートで動作を変えられるようにする
		protected virtual void OnTriggerEnter2D(Collider2D other)
		{
			Enemy enemy = other.GetComponent<Enemy>();
			if (enemy != null && isMovable)
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
					if (isMovable && !isInvincible)
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

		private IEnumerator InvincibilityCoroutine()
		{
			isInvincible = true;
			yield return new WaitForSeconds(hitInterval);
			isInvincible = false;
		}
	}
}
