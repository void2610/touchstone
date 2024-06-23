namespace NManager
{
	using System.Collections;
	using System.Collections.Generic;
	using System.Threading.Tasks;
	using NCharacter;
	using NManager;
	using UnityEngine;
	using UnityEngine.SceneManagement;
	using UnityEngine.UI;

	public class ScoreManager : MonoBehaviour
	{
		public static ScoreManager instance = null;

		private void Awake()
		{
			if (instance == null)
			{
				instance = this;
				DontDestroyOnLoad(this.gameObject);
			}
			else
			{
				Destroy(this.gameObject);
			}
		}

		public int score = 0;

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
			PlayerPrefs.SetInt("score", 0);

			if (!PlayerPrefs.HasKey("highScore"))
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
		}
	}
}
