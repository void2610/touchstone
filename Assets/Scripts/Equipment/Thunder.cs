namespace NEquipment
{
	using System.Collections;
	using System.Collections.Generic;
	using UnityEngine;
	using UnityEngine.UI;
	using NCharacter;

	public class Thunder : Equipment
	{
		[SerializeField]
		private ParticleSystem thunderParticle;

		private float thresholdDistance = 2f;
		protected override void Awake()
		{
			base.Awake();
			equipmentName = "Thunder";
			coolTimeLength = 7.5f;
			activeTimeLength = 3f;
			isHold = false;
		}

		protected override void OnActionStart()
		{
			base.OnActionStart();
			thunderParticle.Play();
			player.GetComponent<Player>().isThunder = true;
		}

		protected override void Effect()
		{
			base.Effect();
			this.transform.position = player.transform.position;

			RaycastHit2D hitR = Physics2D.Raycast(player.transform.position, Vector2.right, thresholdDistance, 1 << LayerMask.NameToLayer("Ground"));
			RaycastHit2D hitL = Physics2D.Raycast(player.transform.position, Vector2.left, thresholdDistance, 1 << LayerMask.NameToLayer("Ground"));
			int dir = hitR.collider != null ? 1 : hitL.collider != null ? -1 : 0;
			if (dir != 0)
			{
				player.GetComponent<Player>().isThunder = true;
				player.GetComponent<Rigidbody2D>().velocity = Vector2.Lerp(player.GetComponent<Rigidbody2D>().velocity, new Vector2(0, 15 * intensity), 0.1f);
			}
			else
			{
				player.GetComponent<Player>().isThunder = false;
			}
		}

		protected override void OnActionEnd()
		{
			base.OnActionEnd();
			thunderParticle.Stop();
			player.GetComponent<Player>().isThunder = false;
		}

		protected override void Start()
		{
			base.Start();
		}
		protected override void FixedUpdate()
		{
			base.FixedUpdate();
		}
	}
}
