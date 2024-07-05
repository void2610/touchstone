namespace NCharacter
{
	using System.Collections;
	using System.Collections.Generic;
	using UnityEngine;
	using UnityEngine.UI;
	using NManager;
	using DG.Tweening;

	public abstract class Enemy : MonoBehaviour
	{
		public string enemyName { get; protected set; }
		public int maxHp { get; protected set; }
		public int hp { get; protected set; }
		public int atk { get; protected set; }
		public int killScore { get; protected set; }
		protected int direction = 1; //-1 = 左  1 = 右
		protected Vector3 startPosition => this.transform.position;
		private bool isQuitting = false;
		protected GameObject deathParticle => Resources.Load<GameObject>("Prefabs/Particle/EnemyDeathParticle");

		public virtual void Attack()
		{
		}

		protected virtual void OnDestroy()
		{
			if (!isQuitting && GameManager.instance.state == GameManager.GameState.Playing)
			{
				Instantiate(deathParticle, this.transform.position, Quaternion.identity);
			}
		}

		protected virtual void Awake()
		{
		}

		protected virtual void Start()
		{
		}

		protected virtual void Update()
		{
		}

		protected virtual void FixedUpdate()
		{
			if (hp <= 0)
			{
				Destroy(this.gameObject);
			}
		}

		protected virtual void OnTriggerEnter2D(Collider2D other)
		{
			Player player = other.gameObject.GetComponent<Player>();
			if (player != null)
			{
				if (player.isMovable)
				{
					Camera.main.GetComponent<CameraMoveScript>().ShakeCamera();
					if (other.transform.position.y > this.transform.position.y)
					{
						player.JumpByEnemy();
						this.hp -= player.atk;
					}
					else
					{
						player.CutHp(atk);
						Vector3 dir = (player.transform.position - this.transform.position).normalized;
						player.AddForce(new Vector2(dir.x, dir.y) * 30);
					}
				}
			}
		}

		private void OnApplicationQuit()
		{
			isQuitting = true;
		}
	}
}
