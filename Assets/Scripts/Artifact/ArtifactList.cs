namespace NArtifact
{
	using System.Collections;
	using System.Collections.Generic;
	using UnityEngine;

	[CreateAssetMenu(fileName = "ArtifactList", menuName = "ScriptableObject/ArtifactList")]
	public class ArtifactList : ScriptableObject
	{
		public List<ArtifactData> artifacts = new List<ArtifactData>();
	}
}