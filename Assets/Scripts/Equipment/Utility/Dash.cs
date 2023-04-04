namespace NEquipment
{
	using System.Collections;
	using System.Collections.Generic;
	using UnityEngine;

	public class Dash : Utility
	{
		private Vector3 moveAngle;

		public void Awake()
		{
			name = "Dash";
			actionKey = "Fire3";
			isCooling = false;
			coolTimeLength = 1.0f;
			isEnable = true;
			isActive = false;
			activeTimeLength = 0.17f;
		}

		public override void Effect()
		{
			moveAngle = new Vector3(Mathf.Cos(activeStartAngle * Mathf.Deg2Rad) * 1.4f, Mathf.Sin(activeStartAngle * Mathf.Deg2Rad), 0);
			player.GetComponent<Rigidbody2D>().velocity = moveAngle * 40;
		}

		public override void OnActionEnd()
		{
			player.GetComponent<Rigidbody2D>().velocity = Vector3.zero;
		}

		public override void Start()
		{
			base.Start();
		}
	}
}
