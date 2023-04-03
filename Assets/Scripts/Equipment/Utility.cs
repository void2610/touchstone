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
			//iconに画像を入れる
			icon = this.GetComponent<SpriteRenderer>().sprite;
		}

		public virtual void Update()
		{
			if (Input.GetButtonDown(actionKey) && isEnable && !isCooling)
			{
				StartCoroutine(Action());
			}

			if (isActive)
			{
				Effect();
			}
			else
			{
			}
		}
	}
}