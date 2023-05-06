namespace NManager
{
	using System.Collections;
	using System.Collections.Generic;
	using System.Threading.Tasks;
	using NCharacter;
	using UnityEngine;
	using UnityEngine.SceneManagement;
	using UnityEngine.UI;

	public class ScoreManager : MonoBehaviour
	{
		public int score = 0;
		private GameObject sText;
		private Character player;

		public void ResetScore()
		{
			PlayerPrefs.SetInt("score", 0);
			PlayerPrefs.SetInt("highScore", 0);
			PlayerPrefs.Save();
		}

		public void SaveScore()
		{
			if (score > PlayerPrefs.GetInt("highScore"))
			{
				PlayerPrefs.SetInt("highScore", score);
			}
			PlayerPrefs.SetInt("score", score);
			PlayerPrefs.Save();
		}

		void Start()
		{
			player = GameObject.Find("Player").GetComponent<Character>();
			sText = GameObject.Find("ScoreText");
			PlayerPrefs.SetInt("score", 0);

			if (PlayerPrefs.GetInt("highScore") == null)
			{
				PlayerPrefs.SetInt("highScore", 0);
			}
			PlayerPrefs.Save();
		}

		void Update()
		{
			//簡易スコアリセット
			if (Input.GetKey(KeyCode.P))
			{
				ResetScore();
			}
			sText.GetComponent<Text>().text = "Score: " + score.ToString();
		}
	}
}
