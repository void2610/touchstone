namespace NUI
{
	using System.Collections;
	using System.Collections.Generic;
	using UnityEngine;
	using UnityEngine.UI;
	using NArtifact;
	public class ArtifactCardScript : MonoBehaviour
	{
		public ArtifactData artifact;
		private Text nameText;
		private Text descriptionText;
		void Start()
		{
			//子オブジェクトの2つめのテキストを取得
			nameText = transform.GetChild(1).GetComponent<Text>();
			descriptionText = transform.GetChild(2).GetComponent<Text>();

			//アーティファクトの名前と説明を表示
			nameText.text = artifact.artifactName;
			descriptionText.text = artifact.artifactDescription;
		}
	}
}