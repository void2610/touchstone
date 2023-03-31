namespace NEquipment
{
	using System.Collections;
	using System.Collections.Generic;
	using UnityEngine;
	using NCharacter;

	public class MeleeWepon : Wepon
	{
		public AnimationCurve animationCurve;

		public float attackDegree = 60f;

		/// <summary>
		/// フェード速度
		/// </summary>
		private float _fadingSpeed = 0.05f;

		private float _curveRate = 0;

		private float attackStartTime = 0;
		public override IEnumerator Action()
		{


			attackStartTime = Time.time;
			isActive = true;
			yield return new WaitForSeconds(activeTimeLength);
			isActive = false;
			isCooling = true;
			yield return new WaitForSeconds(coolTimeLength);
			isCooling = false;
			yield break;
		}

		private void AttackAnimation()
		{
			angle -= animationCurve.Evaluate((Time.time - attackStartTime) / activeTimeLength) * attackDegree;
			transform.eulerAngles = new Vector3(0f, 0f, angle - 45);
			Debug.Log(animationCurve.Evaluate((Time.time - attackStartTime) / activeTimeLength));
			return;
		}

		public virtual void Start()
		{
			name = "Wepon";
			actionKey = "Mouse0";
			coolTimeLength = 1f;
			isCooling = false;
			isEnable = true;
			isActive = false;
			activeTimeLength = 1f;
			attackPower = 1;
			moveRadius = 60;

			icon = Resources.Load<Sprite>("Sprites/Equipment/" + name);
		}

		public virtual void Update()
		{
			//_curveRate = Mathf.Clamp(_curveRate + _fadingSpeed, 0f, 1f);

			MoveWeapon();

			if (Input.GetButtonDown(actionKey) && isEnable && !isCooling)
			{
				StartCoroutine(Action());
			}

			if (isActive)
			{
				AttackAnimation();

			}
		}

		public virtual void FixedUpdate()
		{
			if (isActive && isEnable && !isCooling)
			{
				// 装備の効果を発揮する処理
				Debug.Log("Active");
			}
			if (isCooling)
			{
				// クールタイム中の処理
				Debug.Log("Cooling");
			}
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
