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
		private AfterImageEffect aie;

		protected override void Awake()
		{
			base.Awake();
			equipmentName = "Dash";
			coolTimeLength = 3.5f;
			activeTimeLength = 0.15f;
			isHold = false;
		}

		protected override void Start()
		{
			base.Start();
			aie = player.AddComponent<AfterImageEffect>();
			aie.afterImageInterval = 0.1f;
			aie.afterImageLifetime = 0.7f;
			aie.afterImageColor = new Color(1f, 1f, 1f, 0.5f);
		}

		protected override void OnActionStart()
		{
			base.OnActionStart();
			var angle = new Vector3(Mathf.Cos(activeStartAngle * Mathf.Deg2Rad) * 1.4f, Mathf.Sin(activeStartAngle * Mathf.Deg2Rad), 0);
			player.GetComponent<PlayerParticles>().PlayDashParticle(angle);
			if (aie != null) aie.isCreateAfterImage = true;
			Camera.main.GetComponent<CameraMoveScript>().ShakeCamera(strength: 0.5f);
		}

		protected override void Effect()
		{
			moveAngle = new Vector3(Mathf.Cos(activeStartAngle * Mathf.Deg2Rad) * 1.4f, Mathf.Sin(activeStartAngle * Mathf.Deg2Rad), 0);
			player.GetComponent<Rigidbody2D>().velocity = moveAngle * intensity * 40;
		}

		protected override void OnActionEnd()
		{
			player.GetComponent<Rigidbody2D>().velocity *= 0.5f;
			if (aie != null) aie.isCreateAfterImage = false;
		}
	}
}
