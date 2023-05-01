namespace NEquipment
{
	using System.Collections;
	using System.Collections.Generic;
	using UnityEngine;

	public class Bomb : Utility
	{
		private Vector3 bombAngle;
		private float throwPower = 40;

		public void Awake()
		{
			name = "Bomb";
			actionKey = "Fire3";
			isCooling = false;
			coolTimeLength = 1.0f;
			isEnable = true;
			isActive = false;
			activeTimeLength = 3f;
		}

		public override void OnActionStart()
		{

			throwPower = 40;
			Debug.Log("Bomb");
		}

		public override void Effect()
		{
			bombAngle = new Vector3(Mathf.Cos(activeStartAngle * Mathf.Deg2Rad) * 1.4f, Mathf.Sin(activeStartAngle * Mathf.Deg2Rad), 0);
			this.transform.position = player.transform.position + bombAngle * 1.8f;
			throwPower += 0.5f;
		}

		public override void OnActionEnd()
		{
			Debug.Log(throwPower);
			this.GetComponent<Rigidbody2D>().AddForce(bombAngle * throwPower);
		}

		public override void Start()
		{
			base.Start();
		}
	}
}
