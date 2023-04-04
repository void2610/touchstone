namespace NEquipment
{
	using System.Collections;
	using System.Collections.Generic;
	using UnityEngine;
	using NCharacter;

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
			player.GetComponent<Player>().isInvincible = true;
		}

		public override void Oninvalid()
		{
			Debug.Log("End");
			player.GetComponent<Player>().isInvincible = false;
		}

		public override void Start()
		{
			base.Start();
		}
	}
}
