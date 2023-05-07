namespace NArtifact
{
	using System.Collections;
	using System.Collections.Generic;
	using UnityEngine;

	public abstract class PassiveArtifact : Artifact
	{

		public virtual void Effect()
		{
			Debug.Log("Effect");
		}

		public virtual void Awake()
		{
			base.Awake();
		}

		public virtual void Start()
		{
			base.Start();
		}
	}
}