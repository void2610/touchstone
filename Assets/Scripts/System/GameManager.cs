namespace NManager
{
	using System;
	using System.Collections;
	using System.Collections.Generic;
	using System.Threading.Tasks;
	using UnityEngine;
	using NCharacter;
	using NEquipment;
	using UnityEngine.SceneManagement;

	public class GameManager : MonoBehaviour
	{
		public static GameManager instance = null;

		private void Awake()
		{
			if (instance == null)
			{
				instance = this;
				DontDestroyOnLoad(this.gameObject);

				if (isRandomedSeed)
				{
					seed = (int)DateTime.Now.Ticks;
				}
				UnityEngine.Random.InitState(seed);
				DG.Tweening.DOTween.SetTweensCapacity(tweenersCapacity: 200, sequencesCapacity: 200);
			}
			else
			{
				Destroy(this.gameObject);
			}
		}

		[SerializeField]
		public EquipmentDataList allEquipmentDataList;


		public enum GameState
		{
			Playing,
			Paused,
			Clear,
			OpenArtifact,
			SelectArtifact,
			GameOver,
			Other
		}

		public GameState state { get; set; } = GameState.Playing;

		[SerializeField]
		private bool isRandomedSeed = false;
		[SerializeField]
		private int seed = 42;

		public Player player { get; private set; }
		public GameObject playerObj { get; private set; }
		public float maxAltitude { get; set; } = 0;

		public void SetMaxAltitude(float altitude)
		{
			maxAltitude = Mathf.Max(maxAltitude, altitude);
		}

		public void SetPlayer(GameObject p)
		{
			playerObj = p;
			player = p.GetComponent<Player>();
		}

		public void OnPlayerDeathEvent()
		{
			state = GameState.GameOver;
		}
		public void OnClickArtifactCardEvent()
		{
			state = GameState.Other;
			StartCoroutine(LoadNextScene());
		}
		public IEnumerator LoadNextScene()
		{
			ScoreManager.instance.score += 20 * player.hp;
			ScoreManager.instance.SaveScore();
			Debug.Log("LoadNextScene");
			yield return new WaitForSeconds(1);
			SceneManager.LoadScene("SampleScene");
		}

		private IEnumerator StageClear()
		{
			state = GameState.Other;
			player.isMovable = false;
			playerObj.GetComponent<Rigidbody2D>().constraints = RigidbodyConstraints2D.FreezeAll;
			yield return new WaitForSeconds(1);
			state = GameState.OpenArtifact;
		}

		private IEnumerator PlayerDeath()
		{
			state = GameState.Other;
			player.isMovable = false;
			playerObj.GetComponent<Rigidbody2D>().constraints = RigidbodyConstraints2D.FreezeAll;
			ScoreManager.instance.SaveScore();
			yield return new WaitForSeconds(1);
			SceneManager.LoadScene("GameOverScene");
		}

		void Start()
		{
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
				case GameState.OpenArtifact:
					break;
				case GameState.SelectArtifact:
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
