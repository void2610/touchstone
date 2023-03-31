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
			coolTimeLength = 1f;
			isEnable = true;
			isActive = false;
			activeTimeLength = 1f;
			attackPower = 1;
		}

		public virtual void Update()
		{
			base.Update();
		}

		public virtual void FixedUpdate()
		{
			base.FixedUpdate();
		}
	}
}
