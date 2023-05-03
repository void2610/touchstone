namespace NCharacter
{
	using System.Collections;
	using System.Collections.Generic;
	using UnityEngine;
	using UnityEngine.UI;

	public class Enemy : Character
	{
		private GameObject damageText;

		public void ShowDamageText(int damage, Vector3 position)
		{
			GameObject dt = GameObject.Instantiate(damageText, position, Quaternion.identity);
			GameObject canvas = GameObject.Find("WorldCanvas");
			dt.GetComponent<Text>().text = damage.ToString();
			dt.transform.SetParent(canvas.transform, false);
		}

		public IEnumerator ChangeColortoRed(GameObject target)
		{
			target.GetComponent<SpriteRenderer>().color = Color.red;
			yield return new WaitForSeconds(0.3f);
			target.GetComponent<SpriteRenderer>().color = Color.white;
			yield return null;
		}

		public IEnumerator HitStop()
		{
			Time.timeScale = 0f;
			yield return new WaitForSecondsRealtime(0.1f);
			Time.timeScale = 1;
		}

		//攻撃処理(この中でCutHPを呼ぶ)(キャラによって違う)
		public virtual void Attack()
		{
		}

		public override void Start()
		{
			base.Start();
			name = "Enemy";
			hp = 1;
			atk = 1;
			killScore = 1;
			damageText = Resources.Load<GameObject>("Prefabs/DamageText");
		}

		public override void FixedUpdate()
		{
			base.FixedUpdate();
			if (hp <= 0)
			{
				Destroy(this.gameObject);
			}
		}

		void OnCollisionEnter2D(Collision2D other) //敵に触れた時の処理
		{
			Character target = null;
			if (other.gameObject.tag == "PlayerTrigger")
			{
				target = SearchCharacter(other.gameObject);

				if (!target.isInvincible)
				{
					StartCoroutine(ChangeColortoRed(other.gameObject));
					CutHP(target);
					ShowDamageText(atk, target.transform.position);
					StartCoroutine(HitStop());
				}
			}
		}
	}
}
