namespace NUI
{
	using System.Collections;
	using System.Collections.Generic;
	using UnityEngine;
	using UnityEngine.UI;
	using NArtifact;

	public class SelectArtifactScript : MonoBehaviour
	{
		[SerializeField]
		private ArtifactList allArtifacts;
		[SerializeField]
		private ArtifactList obtainedArtifacts;

		private ArtifactCardScript artifactCard1;
		private ArtifactCardScript artifactCard2;
		private ArtifactCardScript artifactCard3;

		void AddArtifact(ArtifactData artifact)
		{
			//obtainedArtifactsのartifactsリストにartifactを追加する
			obtainedArtifacts.artifacts.Add(artifact);
		}

		void PickThreeArtifact()
		{
			//allArtifactsのartifactsリストからランダムにartifactを取得し、ArtifactCardScriptのartifactに代入する
			ArtifactData artifact = allArtifacts.artifacts[Random.Range(0, allArtifacts.artifacts.Count)];
			artifactCard1.artifact = artifact;
			artifact = allArtifacts.artifacts[Random.Range(0, allArtifacts.artifacts.Count)];
			artifactCard2.artifact = artifact;
			artifact = allArtifacts.artifacts[Random.Range(0, allArtifacts.artifacts.Count)];
			artifactCard3.artifact = artifact;
		}

		void Start()
		{
			artifactCard1 = transform.GetChild(0).GetComponent<ArtifactCardScript>();
			artifactCard2 = transform.GetChild(1).GetComponent<ArtifactCardScript>();
			artifactCard3 = transform.GetChild(2).GetComponent<ArtifactCardScript>();

			//PickThreeArtifactを実行
			PickThreeArtifact();
		}
	}
}