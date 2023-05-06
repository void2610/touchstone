namespace NUI
{
	using System.Collections;
	using System.Collections.Generic;
	using NManager;
	using UnityEngine;
	using UnityEngine.UI;

	public class ShowScoreScript : MonoBehaviour
	{
		void Update()
		{
			Text score_text = gameObject.GetComponent<Text>();

			// テキストの表示を入れ替える
			if (this.name == "ScoreText")
			{
				score_text.text =
					"Score:" + PlayerPrefs.GetInt("score").ToString();
			}
			else if (this.name == "HighScoreText")
			{
				score_text.text =
					"Highcore:" + PlayerPrefs.GetInt("highScore").ToString();
			}
		}
	}
}
