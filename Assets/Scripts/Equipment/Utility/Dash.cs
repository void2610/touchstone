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
			coolTimeLength = 0.1f;
			isEnable = true;
			isActive = false;
			activeTimeLength = 0.3f;
		}

		public override void Effect()
		{


			moveAngle = new Vector3(Mathf.Cos(activeStartAngle * Mathf.Deg2Rad), Mathf.Sin(activeStartAngle * Mathf.Deg2Rad), 0);

			Debug.Log(moveAngle);

			player.GetComponent<Rigidbody2D>().AddForce(moveAngle * 50, ForceMode2D.Impulse);
		}

		public override void Start()
		{
			base.Start();
		}
	}
}