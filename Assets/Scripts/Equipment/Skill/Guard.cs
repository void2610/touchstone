namespace NEquipment
{
	using System.Collections;
	using System.Collections.Generic;
	using UnityEngine;
	using NCharacter;

	public class Guard : Skill
	{
		protected override void Awake()
		{
			name = "Dash";
			actionKey = "Fire2";
			isCooling = false;
			coolTimeLength = 3.0f;
			isEnable = true;
			isActive = false;
			activeTimeLength = 3f;
		}

		protected override void Effect()
		{
			player.GetComponent<Player>().isInvincible = true;
			player.GetComponent<Player>().isMovable = false;
			player.GetComponent<Rigidbody2D>().velocity = new Vector3(0, player.GetComponent<Rigidbody2D>().velocity.y, 0);
			if (player.transform.localScale.x > 0)
			{
				this.transform.position = player.transform.position + new Vector3(1.5f, -0.4f, 0);
			}
			else
			{
				this.transform.position = player.transform.position + new Vector3(-1.5f, -0.4f, 0);
			}
		}

		protected override void OnActionEnd()
		{
			player.GetComponent<Player>().isInvincible = false;
			player.GetComponent<Player>().isMovable = true;
			this.transform.position = new Vector3(0, 0, -10);
		}

		protected override void Start()
		{
			this.transform.position = new Vector3(0, 0, -10);
			base.Start();
		}
	}
}
