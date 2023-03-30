namespace NEquipment
{
	using System.Collections;
	using System.Collections.Generic;
	using UnityEngine;
	using NCharacter;

	public class MeleeWepon : Wepon
	{
		public virtual void Action()
		{
		}

		public virtual IEnumerator Attack()
		{
			isActive = true;
			yield return new WaitForSeconds(activeTimeLength);
			isActive = false;
			isCooling = true;
			yield break;
		}

		public IEnumerator CoolDown()
		{
			yield return new WaitForSeconds(coolTimeLength);
			isCooling = false;
			yield break;
		}

		public virtual void Start()
		{
			base.Start();
			name = "Wepon";
			actionKey = "Mouse0";
			coolTimeLength = 0.1f;
			isActive = false;
		}

		public virtual void Update()
		{
			base.Update();
		}

		public virtual void FixedUpdate()
		{
			base.FixedUpdate();
		}

		//敵に武器が当たったとき
		public void OnTriggerStay2D(Collider2D other)
		{
			Character target = null;
			if (other.gameObject.GetComponent<Character>() != null)
			{
				target = other.gameObject.GetComponent<Character>();
			}
			else
			{
				return;
			}

			if (target.GetType().IsSubclassOf(typeof(Enemy)))
			{
				if (isActive)
				{
					target.hp -= attackPower;
					Debug.Log(target.name + "を攻撃した");

					// if (target.hp <= 0)
					// {
					// 	gcScript.score += target.killScore;
					// }
				}
			}
		}
	}
}
