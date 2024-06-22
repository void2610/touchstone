namespace NCharacter
{
	using System.Collections;
	using System.Collections.Generic;
	using UnityEngine;
	using UnityEngine.UI;
	using NManager;

	public abstract class Enemy : MonoBehaviour
	{
		public string enemyName { get; protected set; }
		public int maxHp { get; protected set; }
		public int hp { get; protected set; }
		public int atk { get; protected set; }
		public int killScore { get; protected set; }
		protected int direction = 1; //-1 = 左  1 = 右
		protected Vector3 startPosition;
		private GameObject damageText;
		protected GameObject deathParticle;

		public void ShowDamageText(int damage, Vector3 position)
		{
			GameObject dt = GameObject.Instantiate(damageText, position, Quaternion.identity);
			GameObject canvas = GameObject.Find("WorldCanvas");
			dt.GetComponent<Text>().text = damage.ToString();
			dt.transform.SetParent(canvas.transform, false);
		}

		public IEnumerator ChangeColortoRed(GameObject tar)
		{
			tar.GetComponent<SpriteRenderer>().color = Color.red;
			yield return new WaitForSeconds(0.3f);
			tar.GetComponent<SpriteRenderer>().color = Color.white;
			yield break;
		}

		public IEnumerator HitStop()
		{
			Time.timeScale = 0f;
			yield return new WaitForSecondsRealtime(0.1f);
			Time.timeScale = 1;
		}

		public virtual void Attack()
		{
		}

		protected virtual void OnDestroy()
		{
			Instantiate(deathParticle, this.transform.position, Quaternion.identity);
		}

		protected virtual void Awake()
		{
		}

		protected virtual void Start()
		{
			damageText = Resources.Load<GameObject>("Prefabs/DamageText");
			deathParticle = Resources.Load<GameObject>("Prefabs/Particle/EnemyDeathParticle");
			startPosition = this.transform.position;
		}

		protected virtual void Update()
		{
		}

		protected virtual void FixedUpdate()
		{
			if (hp <= 0)
			{
				ScoreManager.instance.score += killScore;
				Destroy(this.gameObject);
			}
		}

		protected virtual void OnTriggerEnter2D(Collider2D other)
		{
			Player player = other.gameObject.GetComponent<Player>();
			if (player != null)
			{
				if (other.transform.position.y > this.transform.position.y + 0.5f)
				{
					player.Jump();
					this.hp -= player.atk;
				}
				else
				{
					player.CutHp(atk);
				}
			}
		}
	}
}
