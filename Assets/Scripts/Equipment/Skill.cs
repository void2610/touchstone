namespace NEquipment
{
	using System.Collections;
	using System.Collections.Generic;
	using UnityEngine;

	public class Skill : Equipment
	{
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

		public IEnumerator CoolTime()
		{
			isCooling = true;
			yield return new WaitForSeconds(coolTimeLength);
			isCooling = false;
			yield break;
		}

		public virtual void Effect()
		{
		}

		public virtual void OnActionEnd()
		{
		}

		public virtual void Start()
		{
			base.Start();
		}

		public virtual void Update()
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
		}

		public virtual void FixedUpdate()
		{
			if (isActive)
			{
				Effect();
			}
		}
	}
}