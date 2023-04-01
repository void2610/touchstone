namespace NEquipment
{
	using System.Collections;
	using System.Collections.Generic;
	using UnityEngine;

	public class Sword : MeleeWepon
	{
		public void Awake()
		{
			name = "Sword";
			actionKey = "Fire1";
			coolTimeLength = 0.1f;
			isEnable = true;
			isActive = false;
			activeTimeLength = 0.3f;
			attackPower = 1;
			attackDegree = 50f;
			moveRadius = 60;
		}

		public override void Start()
		{
			base.Start();
			Debug.Log(icon.name);
		}
	}
}
