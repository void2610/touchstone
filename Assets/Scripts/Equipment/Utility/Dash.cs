namespace NEquipment
{
	using System.Collections;
	using System.Collections.Generic;
	using UnityEngine;

	public class Dash : Utility
	{
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
			Debug.Log("Dash");
			player.transform.position += transform.forward * 10;
		}

		public override void Start()
		{
			base.Start();
		}
	}
}