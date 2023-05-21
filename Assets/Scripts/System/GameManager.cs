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

		public static GameManager instance = null;

		public enum GameState
		{
			Playing,
			Paused,
			Clear,
			SelectArtifact,
			GameOver,
			Other
		}
		public GameState state { get; set; } = GameState.Playing;
		private Player player;
		private GameObject playerObj;
		private ScoreManager sm;


		public void OnPlayerDeathEvent()
		{
			state = GameState.GameOver;
		}

		private IEnumerator StageClear()
		{
			state = GameState.Other;
			player.isMovable = false;
			playerObj.GetComponent<Rigidbody2D>().constraints = RigidbodyConstraints2D.FreezeAll;
			yield return new WaitForSeconds(1);
			state = GameState.SelectArtifact;
		}

		private IEnumerator OpenArtifactCard()
		{
			state = GameState.Other;
			//ArtifactCardを表示
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

		void Start()
		{
			playerObj = GameObject.Find("Player");
			player = playerObj.GetComponent<Player>();
			sm = GameObject.Find("GameController").GetComponent<ScoreManager>();
		}

		void Update()
		{
			switch (state)
			{
				case GameState.Playing:
				break;
				case GameState.Paused:
				break;
				case GameState.Clear:
				StartCoroutine(StageClear());
				break;
				case GameState.SelectArtifact:
				StartCoroutine(OpenArtifactCard());
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