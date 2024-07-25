namespace NEquipment
{
	using System.Collections;
	using System.Collections.Generic;
	using UnityEngine;
	using UnityEngine.UI;
	using NCharacter;
	using NManager;

	public class Empty : Equipment
	{
		protected override void Awake()
		{
			base.Awake();
			equipmentName = "Empty";
			coolTimeLength = 999;
			activeTimeLength = 999;
			isHold = false;
		}
	}
}
