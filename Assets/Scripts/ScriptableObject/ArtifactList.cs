namespace NArtifact
{
	using System.Collections;
	using System.Collections.Generic;
	using UnityEngine;
	using System;

	[CreateAssetMenu(fileName = "ArtifactList", menuName = "ScriptableObject/ArtifactList")]
	public class ArtifactList : ScriptableObject
	{
		[SerializeField]
		public List<ArtifactData> artifacts;

		public void ResetToInitial()
		{
			artifacts = new List<ArtifactData>();
		}
	}
}