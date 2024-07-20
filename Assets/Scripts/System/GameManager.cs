namespace NManager
{
	using System;
	using System.Collections;
	using System.Collections.Generic;
	using System.Threading.Tasks;
	using unityroom.Api;
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
			maxAltitude = Mathf.Max(maxAltitude, altitude) - 0.15f;
		}

		public void SetPlayer(GameObject p)
		{
			playerObj = p;
			player = p.GetComponent<Player>();
		}

		public void Retry()
		{
			SceneManager.LoadScene(SceneManager.GetActiveScene().name);
		}

		public void Pause()
		{
			state = GameState.Paused;
			this.GetComponent<UIManager>().ChangeUIState(GameState.Paused);
			Cursor.visible = true;
			Time.timeScale = 0;
			player.isMovable = false;
		}

		public void Resume()
		{
			state = GameState.Playing;
			this.GetComponent<UIManager>().ChangeUIState(GameState.Playing);
			Cursor.visible = false;
			Time.timeScale = 1;
			player.isMovable = true;
		}

		public void GameOver()
		{
			state = GameState.GameOver;
			int gainedCoins = (int)(maxAltitude / 10);
			int currentCoins = PlayerPrefs.GetInt("Coin", 0);
			player.isMovable = false;
			this.GetComponent<UIManager>().ChangeUIState(GameState.GameOver);
			this.GetComponent<UIManager>().SetResultText("max: " + maxAltitude.ToString("F2"));
			this.GetComponent<UIManager>().SetGaindCoinText("+" + gainedCoins.ToString());
			this.GetComponent<EquipmentManager>().ChangeAllEquipmentEnabled(false);
			Cursor.visible = true;
			UnityroomApiClient.Instance.SendScore(1, maxAltitude, ScoreboardWriteMode.Always);
			UnityroomApiClient.Instance.SendScore(2, currentCoins + gainedCoins, ScoreboardWriteMode.Always);
			PlayerPrefs.SetInt("Coin", currentCoins + gainedCoins);
		}

		public void ClearStage()
		{
			Initiate.Fade("ItemScene", Color.black, 1.0f);
		}

		void Start()
		{
			state = GameState.Playing;
			Time.timeScale = 1;
			Cursor.visible = false;
		}

		void Update()
		{
			switch (state)
			{
				case GameState.Playing:
					if (Input.GetKeyDown(KeyCode.P))
					{
						Pause();
					}
					break;
				case GameState.Paused:
					if (Input.GetKeyDown(KeyCode.P))
					{
						Resume();
					}
					break;
				case GameState.GameOver:
					break;
				default:
					break;
			}
		}
	}
}
