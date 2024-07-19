namespace NEquipment
{
	using System.Collections;
	using System.Collections.Generic;
	using UnityEngine;
	using UnityEngine.UI;
	using NCharacter;
	using NManager;

	public class Dash : Equipment
	{
		private Vector3 moveAngle;

		protected override void Awake()
		{
			base.Awake();
			equipmentName = "Dash";
			coolTimeLength = 3.5f;
			activeTimeLength = 0.15f;
			isHold = false;
		}

		protected override void OnActionStart()
		{
			base.OnActionStart();
			var angle = new Vector3(Mathf.Cos(activeStartAngle * Mathf.Deg2Rad) * 1.4f, Mathf.Sin(activeStartAngle * Mathf.Deg2Rad), 0);
			player.GetComponent<PlayerParticles>().PlayDashParticle(angle);
			Camera.main.GetComponent<CameraMoveScript>().ShakeCamera();
		}

		protected override void Effect()
		{
			moveAngle = new Vector3(Mathf.Cos(activeStartAngle * Mathf.Deg2Rad) * 1.4f, Mathf.Sin(activeStartAngle * Mathf.Deg2Rad), 0);
			player.GetComponent<Rigidbody2D>().velocity = moveAngle * intensity * 40;
		}

		protected override void OnActionEnd()
		{
			player.GetComponent<Rigidbody2D>().velocity *= 0.5f;
		}
	}
}
