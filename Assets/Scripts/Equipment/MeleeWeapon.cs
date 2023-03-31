namespace NEquipment
{
	using System.Collections;
	using System.Collections.Generic;
	using UnityEngine;
	using NCharacter;

	public class MeleeWepon : Wepon
	{
		public AnimationCurve animationCurve;

		public float attackDegree;

		/// <summary>
		/// フェード速度
		/// </summary>
		private float _fadingSpeed = 0.05f;

		private float _curveRate = 0;

		private float attackStartTime = 0;
		public virtual IEnumerator Action()
		{
			attackStartTime = Time.time;
			base.Action();
			yield break;
		}

		private void AttackAnimation()
		{
			angle -= animationCurve.Evaluate((Time.time - attackStartTime) / activeTimeLength) * attackDegree;
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
		}

		public virtual void Update()
		{
			_curveRate = Mathf.Clamp(_curveRate + _fadingSpeed, 0f, 1f);
			base.Update();
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
