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
			coolTimeLength = 0.1f;
			isEnable = true;
			isActive = false;
			activeTimeLength = 0.3f;
		}

		public override void Effect()
		{
			// ここにダッシュの処理を書く
			// 例えば、
			player.transform.position += transform.forward * 10;
			// とか
		}

		public override void Start()
		{
			base.Start();
		}
	}
}