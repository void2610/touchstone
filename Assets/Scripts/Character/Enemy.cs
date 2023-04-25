namespace NCharacter
{
	using System.Collections;
	using System.Collections.Generic;
	using UnityEngine;
	using UnityEngine.UI;

	public class Enemy : Character
	{
		private GameObject damegeText;

		public IEnumerator ShowDamageText(int damage, Vector3 position)
		{
			//position = new Vector3(position.x, position.y, 0);
			GameObject dt = GameObject.Instantiate(damegeText, position, Quaternion.identity);
			GameObject canvas = GameObject.Find("WorldCanvas");
			dt.GetComponent<Text>().text = damage.ToString();
			dt.transform.SetParent(canvas.transform, false);

			for (int i = 0; i < 50; i++)
			{
				float up = (50 - i) * 0.0005f;
				dt.transform.position += new Vector3(0, up, 0);
				yield return new WaitForSeconds(0.001f);
			}
			yield return new WaitForSeconds(0.8f);
			Destroy(dt);
		}

		public IEnumerator ChangeColortoRed(GameObject target)
		{
			target.GetComponent<SpriteRenderer>().color = Color.red;
			yield return new WaitForSeconds(0.3f);
			target.GetComponent<SpriteRenderer>().color = Color.white;
			yield return null;
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
			damegeText = Resources.Load<GameObject>("Prefabs/DamegeText");
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
					StartCoroutine(ShowDamageText(atk, target.transform.position));
				}
			}
		}
	}
}
