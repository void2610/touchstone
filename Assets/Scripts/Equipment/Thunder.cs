namespace NEquipment
{
	using System.Collections;
	using System.Collections.Generic;
	using UnityEngine;
	using UnityEngine.UI;
	using NCharacter;
	using NManager;

	public class Thunder : Equipment
	{
		[SerializeField]
		private ParticleSystem thunderParticle;

		protected override void Awake()
		{
			base.Awake();
			equipmentName = "Thunder";
			coolTimeLength = 15f;
			activeTimeLength = 3f;
			isHold = false;
		}

		protected override void OnActionStart()
		{
			base.OnActionStart();
			thunderParticle.Play();
			player.GetComponent<Player>().SetIsThunder(true, this.intensity);
			SoundManager.instance.PlaySe("thunder");
		}

		protected override void Effect()
		{
			base.Effect();
			this.transform.position = player.transform.position;
		}

		protected override void OnActionEnd()
		{
			base.OnActionEnd();
			thunderParticle.Stop();
			player.GetComponent<Player>().SetIsThunder(false, this.intensity);
		}

		protected override void Start()
		{
			base.Start();
			this.transform.position = player.transform.position;
			thunderParticle.Stop();
		}
		protected override void FixedUpdate()
		{
			base.FixedUpdate();
		}
	}
}
