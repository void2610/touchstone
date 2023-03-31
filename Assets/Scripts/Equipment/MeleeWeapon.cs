namespace NEquipment
{
	using System.Collections;
	using System.Collections.Generic;
	using UnityEngine;
	using NCharacter;

	public class MeleeWepon : Wepon
	{
		public AnimationCurve animationCurve;

		public float attackDegree = 1f;

		/// <summary>
		/// フェード速度
		/// </summary>
		private float _fadingSpeed = 0.05f;

		private float _curveRate = 0;

		private float attackStartTime = 0;
		public override IEnumerator Action()
		{
			attackStartTime = Time.time;
			Debug.Log("Action");
			base.Action();
			yield break;
		}

		private void AttackAnimation()
		{
			angle -= animationCurve.Evaluate((Time.time - attackStartTime) / activeTimeLength) * attackDegree;
			transform.eulerAngles = new Vector3(0f, 0f, angle - 45);
			Debug.Log(angle);
			return;
		}

		public virtual void Start()
		{
			base.Start();
			name = "Wepon";
			actionKey = "Mouse0";
			coolTimeLength = 1f;
			isActive = false;
			activeTimeLength = 1f;
			attackPower = 1;
			moveRadius = 60;

		}

		public virtual void Update()
		{
			_curveRate = Mathf.Clamp(_curveRate + _fadingSpeed, 0f, 1f);
			base.Update();
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
