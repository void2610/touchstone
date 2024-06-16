namespace NCharacter
{
	using System.Collections;
	using System.Collections.Generic;
	using UnityEngine;
	using UnityEngine.UI;

	public class Character : MonoBehaviour
	{
		public string characterName;

		public int maxHp;

		public int hp;

		public int atk;

		public int killScore;

		public int direction = 1; //-1 = 左  1 = 右

		public Vector2 firstLScale;

		public bool isInvincible = false;

		//相手のHPを減らす機能
		public void CutHP(Character target)
		{
			if (target.isInvincible == false)
			{
				target.hp -= atk;
				if (target.characterName == "Player")
				{
					StartCoroutine(StartDamageCooldown(target));
				}
			}

		}

		public IEnumerator StartDamageCooldown(Character target)
		{
			target.isInvincible = true;
			yield return new WaitForSeconds(0.5f);
			target.isInvincible = false;
		}

		public Character SearchCharacter(GameObject target)
		{
			Character result = null;

			if (target.GetComponent<Character>() != null)
			{
				result = target.GetComponent<Character>();
			}

			return result;
		}

		protected virtual void Awake()
		{
			characterName = "NoName";
			hp = 1;
			atk = 1;
			killScore = 1;
		}

		protected virtual void Start()
		{
			firstLScale = this.gameObject.transform.localScale;
		}

		protected virtual void Update()
		{
			if (direction == 1)
			{
				this.gameObject.transform.localScale = new Vector2(firstLScale.x, firstLScale.y);
			}
			else
			{
				this.gameObject.transform.localScale = new Vector2(-firstLScale.x, firstLScale.y);
			}
		}

		protected virtual void FixedUpdate()
		{
		}
	}
}
