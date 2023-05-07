namespace NArtifact
{
	using System.Collections;
	using System.Collections.Generic;
	using UnityEngine;

	[CreateAssetMenu(fileName = "ArtifactDataBase", menuName = "CreateArtifactDataBase")]
	public class ArtifactDataBase : ScriptableObject
	{
		public List<Artifact> artifacts = new List<Artifact>();
	}
}