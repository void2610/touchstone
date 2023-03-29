namespace NRoom
{
	using System.Collections;
	using System.Collections.Generic;
	using NControl;
	using UnityEngine;
	using UnityEngine.SceneManagement;

	public class GoalAreaScript : MonoBehaviour
	{
		private GameControlScript gcScript;

		void Start()
		{
			gcScript = GameObject.Find("GameController").GetComponent<GameControlScript>();
		}

		void OnTriggerEnter2D(Collider2D other)
		{
			if (other.gameObject.tag == "PlayerTrigger")
			{
				//ゴール
				gcScript.score += 20;
				gcScript.SaveScore();
				SceneManager.LoadScene("GameOverScene");
			}
		}
	}
}
