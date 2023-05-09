namespace NManager
{
	using System.Collections;
	using System.Collections.Generic;
	using System.Threading.Tasks;
	using UnityEngine;
	using NCharacter;
	using UnityEngine.SceneManagement;

	public class GameManager : MonoBehaviour
	{
		public enum GameState
		{
			Playing,
			Paused,
			Clear,
			GameOver,
			Other
		}
		private Player player;
		private GameObject playerObj;
		private ScoreManager sm;
		public GameState state { get; set; } = GameState.Playing;

		private IEnumerator StageClear()
		{
			state = GameState.Other;
			player.isMovable = false;
			playerObj.GetComponent<Rigidbody2D>().constraints = RigidbodyConstraints2D.FreezeAll;
			sm.score += 20 * player.hp;
			sm.SaveScore();
			yield return new WaitForSeconds(1);
			SceneManager.LoadScene("GameOverScene");
		}

		private IEnumerator PlayerDeath()
		{
			player.isMovable = false;
			playerObj.GetComponent<Rigidbody2D>().constraints = RigidbodyConstraints2D.FreezeAll;
			sm.SaveScore();
			yield return new WaitForSeconds(1);
			SceneManager.LoadScene("GameOverScene");
		}
		void Start()
		{
			playerObj = GameObject.Find("Player");
			player = playerObj.GetComponent<Player>();
			sm = GameObject.Find("GameController").GetComponent<ScoreManager>();
		}

		// Update is called once per frame
		void Update()
		{
			if (player.hp <= 0)
			{
				state = GameState.GameOver;
			}


			switch (state)
			{
				case GameState.Playing:
				break;
				case GameState.Paused:
				break;
				case GameState.Clear:
				StartCoroutine(StageClear());
				break;
				case GameState.GameOver:
				StartCoroutine(PlayerDeath());
				break;
				default:
				break;
			}
		}
	}
}