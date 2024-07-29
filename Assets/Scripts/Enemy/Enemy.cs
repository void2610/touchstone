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
		protected GameObject deathParticle => Resources.Load<GameObject>("Prefabs/Particle/EnemyDeathParticle");

		public virtual void Attack()
		{
		}

		public virtual void CutHp(int damage)
		{
			hp -= damage;
			if (hp <= 0)
			{
				InstantiateDeathParticle();
				Destroy(this.gameObject);
			}
		}

		public void InstantiateDeathParticle()
		{
			Instantiate(deathParticle, this.transform.position, Quaternion.identity);
		}

		protected virtual void OnDestroy()
		{
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
		}
	}
}
