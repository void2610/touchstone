namespace NArtifact
{
	using System.Collections;
	using System.Collections.Generic;
	using UnityEngine;

	public abstract class Artifact : MonoBehaviour
	{
		public string name;
		public string description;
		//public Sprite icon;

		public virtual void Awake()
		{
			name = "NoName";
			description = "NoDescription";
		}
		public virtual void Start()
		{

		}

		public virtual void Update()
		{

		}
	}
}