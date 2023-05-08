namespace NArtifact
{
	using System.Collections;
	using System.Collections.Generic;
	using UnityEngine;
	using UnityEngine.UI;

	[CreateAssetMenu(fileName = "ArtifactData", menuName = "ScriptableObject/ArtifactData")]
	public class ArtifactData : ScriptableObject
	{
		public string artifactName;
		public string artifactDescription;
		public Image artifactIcon;
	}
}