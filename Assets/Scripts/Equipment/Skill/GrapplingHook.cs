namespace NEquipment
{
	using System.Collections;
	using System.Collections.Generic;
	using UnityEngine;

	public class GrapplingHook : Skill
	{
		private float hookSpeed = 10;
		private float hookLength = 5;
		private float hookPullSpeed = 5;

		private Vector3 movePosition;
		public void Awake()
		{
			name = "GrapplingHook";
			actionKey = "Fire2";
			isCooling = false;
			coolTimeLength = 1.0f;
			isEnable = true;
			isActive = false;
			activeTimeLength = 5f;
		}

		//ボタンを長押ししている間は常にEffect()が呼ばれる
		//
		public override void Effect()
		{
			// moveAngle = new Vector3(Mathf.Cos(activeStartAngle * Mathf.Deg2Rad) * 1.4f, Mathf.Sin(activeStartAngle * Mathf.Deg2Rad), 0);
			// player.GetComponent<Rigidbody2D>().velocity = moveAngle * 40;
		}

		public override void Oninvalid()
		{
			player.GetComponent<Rigidbody2D>().velocity = Vector3.zero;
		}

		public override void Start()
		{
			base.Start();
		}
	}
}