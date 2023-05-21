namespace NUI
{
	using System.Collections;
	using System.Collections.Generic;
	using UnityEngine;
	using UnityEngine.UI;
	using NArtifact;
	using NManager;

	public class SelectArtifactScript : MonoBehaviour
	{
		public GameEvent OnClickArtifactCardEvent;
		[SerializeField]
		private ArtifactList allArtifacts;
		[SerializeField]
		private ArtifactList obtainedArtifacts;

		private ArtifactCardScript artifactCard1;
		private ArtifactCardScript artifactCard2;
		private ArtifactCardScript artifactCard3;

		public void OnClickArtifactCard1()
		{
			AddArtifact(artifactCard1.artifact);
			OnClickArtifactCardEvent.Raise();
		}

		public void OnClickArtifactCard2()
		{
			AddArtifact(artifactCard2.artifact);
			OnClickArtifactCardEvent.Raise();
		}

		public void OnClickArtifactCard3()
		{
			AddArtifact(artifactCard3.artifact);
			OnClickArtifactCardEvent.Raise();
		}

		private void AddArtifact(ArtifactData artifact)
		{
			//obtainedArtifactsのartifactsリストにartifactを追加する
			obtainedArtifacts.artifacts.Add(artifact);
			Debug.Log("AddArtifact: " + obtainedArtifacts.artifacts.Count);
		}

		public void PickThreeArtifact()
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
			Random.InitState(System.DateTime.Now.Millisecond);
			artifactCard1 = transform.GetChild(0).GetComponent<ArtifactCardScript>();
			artifactCard2 = transform.GetChild(1).GetComponent<ArtifactCardScript>();
			artifactCard3 = transform.GetChild(2).GetComponent<ArtifactCardScript>();

			artifactCard1.gameObject.SetActive(false);
			artifactCard2.gameObject.SetActive(false);
			artifactCard3.gameObject.SetActive(false);
		}
		void Update()
		{
			if (GameManager.instance.state == GameManager.GameState.OpenArtifact)
			{
				//PickThreeArtifactを実行
				PickThreeArtifact();

				artifactCard1.gameObject.SetActive(true);
				artifactCard2.gameObject.SetActive(true);
				artifactCard3.gameObject.SetActive(true);

				GameManager.instance.state = GameManager.GameState.SelectArtifact;
			}
		}
	}
}