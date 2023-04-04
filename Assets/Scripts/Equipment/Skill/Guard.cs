namespace NEquipment
{
	using System.Collections;
	using System.Collections.Generic;
	using UnityEngine;

	public class Guard : Skill
	{
		public void Awake()
		{
			name = "Dash";
			actionKey = "Fire2";
			isCooling = false;
			coolTimeLength = 3.0f;
			isEnable = true;
			isActive = false;
			activeTimeLength = 3f;
		}

		public override void Effect()
		{
			Debug.Log("Guard");
		}

		public override void Oninvalid()
		{
			Debug.Log("End");
		}

		public override void Start()
		{
			base.Start();
		}
	}
}
