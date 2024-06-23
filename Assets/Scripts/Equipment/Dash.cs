namespace NEquipment
{
	using System.Collections;
	using System.Collections.Generic;
	using UnityEngine;
	using UnityEngine.UI;
	using NCharacter;

	public class Dash : Equipment
	{
		private Vector3 moveAngle;

		protected override void Awake()
		{
			base.Awake();
			equipmentName = "Dash";
			coolTimeLength = 3.5f;
			activeTimeLength = 0.15f;
		}

		protected override void OnActionStart()
		{
			base.OnActionStart();
			player.GetComponent<PlayerParticles>().PlayDashParticle(activeTimeLength);
		}

		protected override void Effect()
		{
			moveAngle = new Vector3(Mathf.Cos(activeStartAngle * Mathf.Deg2Rad) * 1.4f, Mathf.Sin(activeStartAngle * Mathf.Deg2Rad), 0);
			player.GetComponent<Rigidbody2D>().velocity = moveAngle * 40;
		}

		protected override void OnActionEnd()
		{
			player.GetComponent<Rigidbody2D>().velocity *= 0.5f;
		}

		protected override void Start()
		{
			base.Start();
		}
	}
}
