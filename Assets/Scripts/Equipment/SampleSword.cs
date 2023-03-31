namespace NEquipment
{
	using System.Collections;
	using System.Collections.Generic;
	using UnityEngine;

	public class SampleSword : MeleeWepon
	{
		public override void Start()
		{
			base.Start();
			name = "SampleSword";
			actionKey = "Fire1";
			coolTimeLength = 0.1f;
			isEnable = true;
			isActive = false;
			activeTimeLength = 0.3f;
			attackPower = 1;
			attackDegree = 50f;
		}

		// public virtual void Update()
		// {
		// 	base.Update();
		// }

		// public virtual void FixedUpdate()
		// {
		// 	base.FixedUpdate();
		// }
	}
}
