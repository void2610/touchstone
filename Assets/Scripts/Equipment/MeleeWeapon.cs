namespace NEquipment
{
	using System.Collections;
	using System.Collections.Generic;
	using UnityEngine;
	using NCharacter;
	using UnityEngine.UI;

	public class MeleeWeapon : Weapon
	{
		public AnimationCurve animationCurve;

		public float attackDegree = 60f;

		public GameObject gauge;

		private GameObject damegeText;

		private float attackCoolTime = 0;
		private float attackCoolTimeLength = 0.5f;

		public override IEnumerator Action()
		{
			activeStartTime = Time.time;
			isActive = true;
			activeStartAngle = angle;
			OnActionStart();
			yield return new WaitForSeconds(activeTimeLength);
			isActive = false;
			OnActionEnd();
			isCooling = true;
			coolStartTime = Time.time;
			yield return new WaitForSeconds(coolTimeLength);
			isCooling = false;
			yield break;
		}

		protected override void Effect()
		{
			base.Effect();
			if (Mathf.Abs(activeStartAngle) < 90)
			{
				angle = activeStartAngle - animationCurve.Evaluate(((Time.time - activeStartTime) / activeTimeLength)) * attackDegree;
			}
			else
			{
				angle = activeStartAngle + animationCurve.Evaluate(((Time.time - activeStartTime) / activeTimeLength)) * attackDegree;
			}
			var radian = angle * (Mathf.PI / 180);
			transform.position = new Vector3(Mathf.Cos(radian) * moveRadius + 10, Mathf.Sin(radian) * moveRadius - 25, 0).normalized + playerPosition;
			transform.eulerAngles = new Vector3(0f, 0f, angle - 45);
			return;
		}

		protected override void OnActionStart()
		{
			base.OnActionStart();
		}

		protected override void Start()
		{
			base.Start();
			gauge = GameObject.Find("WeaponGauge");
			damegeText = Resources.Load<GameObject>("Prefabs/DamegeText");
		}

		protected override void Update()
		{
			base.Update();
			MoveWeaponPosition();

			if (Input.GetButtonDown(actionKey) && isEnable && !isCooling)
			{
				StartCoroutine(Action());
			}

			if (isActive && isEnable && !isCooling)
			{
				Effect();
			}
			else
			{
				MoveWeaponAngle();
			}

			if (isCooling)
			{
				gauge.GetComponent<Image>().fillAmount = 1 - ((Time.time - coolStartTime) / coolTimeLength);
			}
		}

		protected override void FixedUpdate()
		{
			base.FixedUpdate();
			if (isActive && isEnable && !isCooling)
			{
				// 装備の効果を発揮する処理
				//Debug.Log("Active");
			}
			if (isCooling)
			{
				// クールタイム中の処理
				//Debug.Log("Cooling");
			}
		}

		//敵に武器が当たったとき
		protected void OnTriggerStay2D(Collider2D other)
		{
			if (other.gameObject.GetComponent<Character>() == null)
			{
				return;
			}

			if (other.gameObject.GetComponent<Character>().GetType().IsSubclassOf(typeof(Enemy)))
			{
				if (isActive && Time.time - attackCoolTime > attackCoolTimeLength)
				{
					ProvideDamage(other.gameObject);
					StartCoroutine(HitStop());
					attackCoolTime = Time.time;
				}
			}
		}
	}
}
