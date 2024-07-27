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
	using UnityEngine.SceneManagement;

	public class GameManager : MonoBehaviour
	{
		public static GameManager instance = null;


		// TODO: エンドレスのprefsと分ける
		// TODO: 装備選択がおかしい、選択していないものが選択されている
		// TODO: マルチシーン読み込み時に固まる 事前にロードできないか
		// TODO: 再セットアップ後にフェードが一瞬出る
		//  The object of type 'UnityEngine.UI.Image' has been destroyed but you are still trying to access it.

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
				DG.Tweening.DOTween.SetTweensCapacity(tweenersCapacity: 200, sequencesCapacity: 200);
			}
			else
			{
				Destroy(this.gameObject);
			}
		}

		[SerializeField]
		private bool isEndless = false;
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

		public System.Random random { get; private set; }
		public Player player { get; private set; }
		public GameObject playerObj { get; private set; }
		public float maxAltitude { get; private set; } = 0;
		public float altitudeOffset { get; private set; } = 0;

		private int seed = 42;

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
			this.GetComponent<EquipmentManager>().ChangeAllEquipmentEnabled(false);
			state = GameState.GameOver;
			int gainedCoins = (int)(maxAltitude / 10);
			int currentCoins = PlayerPrefs.GetInt("Coin", 0);
			player.isMovable = false;
			player.isInvincible = true;
			this.GetComponent<UIManager>().ChangeUIState(GameState.GameOver);
			this.GetComponent<UIManager>().SetResultText("max: " + maxAltitude.ToString("F2"));
			this.GetComponent<UIManager>().SetGaindCoinText("+" + gainedCoins.ToString());
			this.GetComponent<EquipmentManager>().ChangeAllEquipmentEnabled(false);
			Cursor.visible = true;

			if (PlayerPrefs.GetInt("RandomSeed", 1) == 1)
			{
				UnityroomApiClient.Instance.SendScore(1, maxAltitude, ScoreboardWriteMode.Always);
				UnityroomApiClient.Instance.SendScore(2, currentCoins + gainedCoins, ScoreboardWriteMode.Always);
			}
			else
			{
				Debug.Log("Seed is used, not send score");
			}
			PlayerPrefs.SetInt("Coin", currentCoins + gainedCoins);
		}

		public void ClearStage()
		{
			player.isMovable = false;
			player.isOnGame = false;
			state = GameState.Selecting;
			altitudeOffset = maxAltitude;
			SceneManager.LoadScene("ItemScene", LoadSceneMode.Additive);
			this.GetComponent<EquipmentManager>().ChangeAllEquipmentEnabled(false);
		}

		public void SetUp()
		{
			state = GameState.Playing;
			Time.timeScale = 1;
			Cursor.visible = false;
			player.isMovable = true;
			player.isOnGame = true;
			state = GameState.Playing;

			player.transform.position = new Vector3(0, 0, 0);
			this.GetComponent<MapManager>()?.SetUp();
			this.GetComponent<EquipmentManager>().SetUp();
			this.GetComponent<EquipmentManager>().ChangeAllEquipmentEnabled(true);
		}

		void Start()
		{
			this.SetUp();
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
				case GameState.Selecting:
					break;
				default:
					break;
			}
		}
	}
}
