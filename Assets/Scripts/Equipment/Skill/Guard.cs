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
			player.GetComponent<Player>().isInvincible = true;
			player.GetComponent<Player>().isMovable = false;
		}

		public override void Oninvalid()
		{
			player.GetComponent<Player>().isInvincible = false;
			player.GetComponent<Player>().isMovable = true;
		}

		public override void Start()
		{
			base.Start();
		}
	}
}
