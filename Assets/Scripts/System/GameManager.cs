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
	using NMap;
	using NUI;
	using UnityEngine.SceneManagement;
	using UnityEngine.InputSystem;

	public class GameManager : MonoBehaviour
	{
		public static GameManager instance = null;

		// TODO: マルチシーン読み込み時に固まる 事前にロードできないか

		private void Awake()
		{
			if (instance == null)
			{
				instance = this;

				if (PlayerPrefs.GetInt("RandomSeed", 1) == 1)
				{
					seed = (int)DateTime.Now.Ticks;
					Debug.Log("Random");
				}
				else
				{
					seed = PlayerPrefs.GetInt("Seed", seed);
					Debug.Log("Seed: " + seed);
				}
				random = new System.Random(seed);
				for (int i = 0; i < StageSize; i++)
				{
					mapRandomSeeds.Add(random.Next());
				}
				DG.Tweening.DOTween.SetTweensCapacity(tweenersCapacity: 200, sequencesCapacity: 200);
			}
			else
			{
				Destroy(this.gameObject);
			}
		}

		[SerializeField]
		public bool isEndless = false;
		[SerializeField]
		public EquipmentDataList allEquipmentDataList;

		public enum GameState
		{
			Playing,
			Paused,
			GameOver,
			Selecting,
			Other
		}

		[SerializeField]
		public GameState state = GameState.Playing;
		[SerializeField]
		public int StageSize = 10;

		public System.Random random { get; private set; }
		public List<int> mapRandomSeeds { get; private set; } = new List<int>();
		public Player player { get; private set; }
		public GameObject playerObj { get; private set; }
		public float maxAltitude { get; private set; } = 0;
		public float altitudeOffset { get; private set; } = 0;

		private int seed = 42;
		private PlayerInput playerInput => this.GetComponent<PlayerInput>();
		private InputAction pause;
		private bool isFirst = true;

		public float RandomRange(float min, float max)
		{
			float randomValue = (float)(this.random.NextDouble() * (max - min) + min);
			return randomValue;
		}

		public int RandomRange(int min, int max)
		{
			int randomValue = this.random.Next(min, max);
			return randomValue;
		}

		public void SetMaxAltitude(float altitude)
		{
			maxAltitude = Mathf.Max(maxAltitude, (altitude + altitudeOffset));
		}

		public void SetPlayer(GameObject p)
		{
			playerObj = p;
			player = p.GetComponent<Player>();
		}

		public void Retry()
		{
			this.GetComponent<UIManager>().FadeIn(SceneManager.GetActiveScene().name);
		}

		public void Pause()
		{
			BGMManager.instance.EnableLowPassFilter();
			playerInput.actions.Disable();
			pause.Enable();
			state = GameState.Paused;
			this.GetComponent<UIManager>().ChangeUIState(GameState.Paused);
			Cursor.visible = true;
			Time.timeScale = 0;
			player.ChangeMovable(false);
		}

		public void Resume()
		{
			BGMManager.instance.DisableLowPassFilter();
			playerInput.actions.Enable();
			state = GameState.Playing;
			this.GetComponent<UIManager>().ChangeUIState(GameState.Playing);
			Cursor.visible = false;
			Time.timeScale = 1;
			player.ChangeMovable(true);
		}

		public void GameOver()
		{
			if (this.state == GameState.GameOver) return;

			Camera.main.GetComponent<CameraMoveScript>().isTracking = false;
			BGMManager.instance.EnableLowPassFilter();
			this.GetComponent<EquipmentManager>().ChangeAllEquipmentEnabled(false);
			state = GameState.GameOver;
			int gainedCoins = (int)(maxAltitude / 10);
			int currentCoins = PlayerPrefs.GetInt("Coin", 0);
			player.ChangeMovable(false);
			player.isInvincible = true;
			this.GetComponent<UIManager>().ChangeUIState(GameState.GameOver);
			this.GetComponent<UIManager>().SetResultText(maxAltitude);
			this.GetComponent<UIManager>().SetGaindCoinText("+" + gainedCoins.ToString());
			this.GetComponent<EquipmentManager>().ChangeAllEquipmentEnabled(false);
			Cursor.visible = true;

			if (PlayerPrefs.GetInt("RandomSeed", 1) == 1)
			{
				if (UnityroomApiClient.Instance != null)
				{
					int boardId = isEndless ? 2 : 1;
					UnityroomApiClient.Instance.SendScore(boardId, maxAltitude, ScoreboardWriteMode.HighScoreDesc);
				}
			}
			else
			{
				Debug.Log("Seed is used, not send score");
			}
			PlayerPrefs.SetInt("Coin", currentCoins + gainedCoins);
		}

		public void ClearStage()
		{
			player.ChangeMovable(false);
			player.isOnGame = false;
			state = GameState.Selecting;
			SceneManager.LoadScene("ItemScene", LoadSceneMode.Additive);
			this.GetComponent<EquipmentManager>().ChangeAllEquipmentEnabled(false);
		}

		public void ResetAltitude()
		{
			maxAltitude = altitudeOffset;
		}

		public void SetUp()
		{
			// if (!isFirst) this.GetComponent<BlessManager>().GetRandomBless();
			Debug.Log(isFirst);
			altitudeOffset = maxAltitude;
			state = GameState.Playing;
			Time.timeScale = 1;
			Cursor.visible = false;
			player.ChangeMovable(true);
			player.isOnGame = true;
			state = GameState.Playing;

			player.transform.position = new Vector3(0, 0, 0);
			this.GetComponent<MapManager>()?.SetUp(!isFirst);
			this.GetComponent<EquipmentManager>().SetUp();
			this.GetComponent<EquipmentManager>().ChangeAllEquipmentEnabled(true);
			isFirst = false;
		}

		void Start()
		{
			pause = playerInput.actions["Pause"];
			this.SetUp();
		}

		void Update()
		{
			switch (state)
			{
				case GameState.Playing:
					if (pause.WasPressedThisFrame())
					{
						Pause();
					}
					break;
				case GameState.Paused:
					if (pause.WasPressedThisFrame())
					{
						Resume();
					}
					break;
				case GameState.GameOver:
					break;
				case GameState.Selecting:
					break;
				default:
					break;
			}
		}
	}
}
