namespace NEquipment
{
	using System.Collections;
	using System.Collections.Generic;
	using UnityEngine;
	using UnityEngine.UI;

	public class Utility : Equipment
	{
		public GameObject gauge;

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

		public virtual void Effect()
		{
		}

		public virtual void OnActionStart()
		{
		}

		public virtual void Start()
		{
			base.Start();
			gauge = GameObject.Find("UtilityGauge");
		}

		public virtual void Update()
		{
			angle = getMouseAngle();
			if (Input.GetButtonDown(actionKey) && isEnable && !isCooling)
			{
				Debug.Log("Utility");
				StartCoroutine(Action());
			}

			if (isCooling)
			{
				gauge.GetComponent<Image>().fillAmount = 1 - ((Time.time - coolStartTime) / coolTimeLength);
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