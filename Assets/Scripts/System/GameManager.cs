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

		public void GameOver()
		{
			state = GameState.GameOver;
			player.isMovable = false;
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
				case GameState.GameOver:
					break;
				default:
					break;
			}
		}
	}
}
