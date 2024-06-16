namespace NEquipment
{
	using System.Collections;
	using System.Collections.Generic;
	using UnityEngine;
	using UnityEngine.UI;

	public class Skill : Equipment
	{
		public GameObject gauge;


		//長押しでactiveTimeLengthの時間まで有効、長押しを離したらクールタイムが始まる
		public override IEnumerator Action()
		{
			activeStartTime = Time.time;
			isActive = true;
			activeStartAngle = angle;
			activeStartPosition = getMousePosition();
			OnActionStart();
			yield return new WaitForSeconds(activeTimeLength);
			if (isActive)
			{
				isActive = false;
				OnActionEnd();
				StartCoroutine(CoolTime());
			}
		}

		protected IEnumerator CoolTime()
		{
			isCooling = true;
			coolStartTime = Time.time;
			yield return new WaitForSeconds(coolTimeLength);
			isCooling = false;
			yield break;
		}

		protected override void Effect()
		{
			base.Effect();
		}

		protected override void OnActionEnd()
		{
			base.OnActionEnd();
		}

		protected override void Awake()
		{
			base.Awake();
		}

		protected override void Start()
		{
			base.Start();
			if (actionKey == "Fire2")
			{
				gauge = GameObject.Find("Skill1Gauge");
			}
			else if (actionKey == "Fire3")
			{
				gauge = GameObject.Find("Skill2Gauge");
			}
		}

		protected override void Update()
		{
			angle = getMouseAngle();
			if (Input.GetButton(actionKey) && isEnable && !isCooling && !isActive)
			{
				StartCoroutine(Action());
			}

			if (isActive && Input.GetButtonUp(actionKey))
			{
				//長押しを離したらクールタイムが始まる
				isActive = false;
				activeStartTime = 0;
				OnActionEnd();
				StartCoroutine(CoolTime());
			}

			if (isCooling)
			{
				gauge.GetComponent<Image>().fillAmount = 1 - ((Time.time - coolStartTime) / coolTimeLength);
			}
		}

		protected override void FixedUpdate()
		{
			if (isActive)
			{
				Effect();
			}
		}
	}
}
