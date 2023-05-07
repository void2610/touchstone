namespace NArtifact
{
	using System.Collections;
	using System.Collections.Generic;
	using UnityEngine;
	using NCharacter;

	public class Vessels_of_Heart : PassiveArtifact
	{
		private Player player;
		public override void Awake()
		{
			base.Awake();
			name = "Vessels of Heart";
			description = "Increase the maximum value of hearts by 1.";
		}

		public override void Effect()
		{
			base.Effect();
			player.maxHp += 2;
		}

		public override void Start()
		{
			base.Start();
			player = GameObject.Find("Player").GetComponent<Player>();
		}
	}
}