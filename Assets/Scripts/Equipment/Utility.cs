namespace NEquipment
{
	using System.Collections;
	using System.Collections.Generic;
	using UnityEngine;

	public class Utility : Equipment
	{
		public override IEnumerator Action()
		{
			activeStartTime = Time.time;
			isActive = true;
			activeStartAngle = angle;
			yield return new WaitForSeconds(activeTimeLength);
			isActive = false;
			isCooling = true;
			yield return new WaitForSeconds(coolTimeLength);
			isCooling = false;
			yield break;
		}

		public virtual void Effect()
		{
		}

		public virtual void Start()
		{
			base.Start();
		}

		public virtual void Update()
		{
			if (Input.GetButtonDown(actionKey) && isEnable && !isCooling)
			{
				Debug.Log("Utility");
				StartCoroutine(Action());
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