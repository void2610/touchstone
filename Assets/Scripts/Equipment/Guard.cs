namespace NEquipment
{
	using System.Collections;
	using System.Collections.Generic;
	using UnityEngine;
	using NCharacter;

	public class Guard : Equipment
	{
		protected override void Awake()
		{
			name = "Guard";
			isCooling = false;
			coolTimeLength = 3.0f;
			isEnable = true;
			isActive = false;
			activeTimeLength = 3f;
			isHold = false;
		}

		protected override void OnActionStart()
		{
			player.GetComponent<Player>().isShield = true;
			this.GetComponent<SpriteRenderer>().color = new Color(1, 1, 1, 1f);
		}

		protected override void Effect()
		{

		}

		protected override void OnActionEnd()
		{
			player.GetComponent<Player>().isShield = false;
			this.GetComponent<SpriteRenderer>().color = new Color(1, 1, 1, 0.0f);
		}

		protected override void Start()
		{
			this.GetComponent<SpriteRenderer>().color = new Color(1, 1, 1, 0.0f);
			base.Start();
		}

		protected override void Update()
		{
			base.Update();
			if (isActive)
			{
				if (player.transform.localScale.x > 0)
				{
					this.transform.position = player.transform.position + new Vector3(1.5f, -0.4f, 0);
				}
				else
				{
					this.transform.position = player.transform.position + new Vector3(-1.5f, -0.4f, 0);
				}
			}
		}
	}
}
