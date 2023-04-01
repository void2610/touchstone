namespace NEquipment
{
	using System.Collections;
	using System.Collections.Generic;
	using UnityEngine;

	public class Hammer : MeleeWepon
	{
		public void Awake()
		{
			name = "Hammer";
			actionKey = "Fire1";
			coolTimeLength = 0.3f;
			isEnable = true;
			isActive = false;
			activeTimeLength = 0.9f;
			attackPower = 3;
			attackDegree = 80f;
			moveRadius = 90;
		}

		public override void Start()
		{
			base.Start();
		}
	}
}
