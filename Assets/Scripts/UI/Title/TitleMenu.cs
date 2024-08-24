namespace NTitle
{
	using System.Collections;
	using System.Collections.Generic;
	using UnityEngine;
	using UnityEngine.SceneManagement;
	using UnityEngine.UI;
	using UnityEngine.EventSystems;
	using unityroom.Api;
	using NEquipment;
	using TMPro;
	using NManager;

	public class TitleMenu : MonoBehaviour
	{
		[SerializeField]
		private EquipmentDataList allEquipments;
		[SerializeField]
		private List<CanvasGroup> canvasGroups = new List<CanvasGroup>();
		[SerializeField]
		private TitleEquipmentContainer e1;
		[SerializeField]
		private TitleEquipmentContainer e2;
		[SerializeField]
		private TitleEquipmentContainer e3;
		[SerializeField]
		private Slider bgmSlider;
		[SerializeField]
		private Slider seSlider;
		[SerializeField]
		private Toggle randomSeedToggle;
		[SerializeField]
		private TMP_InputField seedInputField;
		[SerializeField]
		private TMP_Text placeHolder;
		[SerializeField]
		private TMP_Text seedText;
		[SerializeField]
		private GameObject keyBindElements;
		[SerializeField]
		private AudioClip bgmAudioClip;

		[SerializeField]
		private CanvasGroup credit;
		[SerializeField]
		private CanvasGroup license;

		public bool isRandomedSeed { get { return seedInputField.interactable; } set { seedInputField.interactable = value; } }

		private int state = 0;
		//0:タイトル画面 1:装備編成画面 2:設定画面 3:装備選択画面
		private int selectEquip = 0;

		public void ShowCredit()
		{
			SoundManager.instance.PlaySe("button");
			credit.alpha = 1;
			credit.interactable = true;
			credit.blocksRaycasts = true;
		}

		public void ShowLicense()
		{
			SoundManager.instance.PlaySe("button");
			license.alpha = 1;
			license.interactable = true;
			license.blocksRaycasts = true;
		}

		public void CloseCredit()
		{
			PlayButtonSe();
			credit.alpha = 0;
			credit.interactable = false;
			credit.blocksRaycasts = false;
		}

		public void CloseLicense()
		{
			PlayButtonSe();
			license.alpha = 0;
			license.interactable = false;
			license.blocksRaycasts = false;
		}

		public void OnClickStartButton()
		{
			SoundManager.instance.PlaySe("button");
			Initiate.Fade("SampleScene", Color.black, 1.0f);
		}

		public void OnClickEndlessButton()
		{
			SoundManager.instance.PlaySe("button");
			ChangeState(1);
		}

		public void OnClickSettingButton()
		{
			SoundManager.instance.PlaySe("button");
			ChangeState(2);
		}

		public void OnClickControlSettingButton()
		{
			SoundManager.instance.PlaySe("button");
			ChangeState(4);
		}

		public void OnClickBackButton()
		{
			SoundManager.instance.PlaySe("button");
			ChangeState(0);
		}

		public void OnClickQuitButton()
		{
			SoundManager.instance.PlaySe("button");
			//ゲーム終了
			Application.Quit();
			//エディターでプレイモードを終了
			// UnityEditor.EditorApplication.isPlaying = false;
		}

		public void BackToSetting()
		{
			SoundManager.instance.PlaySe("button");
			ChangeState(2);
		}

		public void OnClickMainEntryButton()
		{
			//ゲームシーンへ
			SoundManager.instance.PlaySe("button");
			Initiate.Fade("MainScene", Color.black, 1.0f);
		}

		public void OnClickEndlessEntryButton()
		{
			//ゲームシーンへ
			SoundManager.instance.PlaySe("button");
			Initiate.Fade("EndlessScene", Color.black, 1.0f);
		}

		public void OnClickEquip1Button()
		{
			selectEquip = 1;
			SoundManager.instance.PlaySe("button");
			ChangeState(3);
		}
		public void OnClickEquip2Button()
		{
			selectEquip = 2;
			SoundManager.instance.PlaySe("button");
			ChangeState(3);
		}
		public void OnClickEquip3Button()
		{
			selectEquip = 3;
			SoundManager.instance.PlaySe("button");
			ChangeState(3);
		}

		public void OnClickEquipButton(int i)
		{
			if (selectEquip == 1)
			{
				PlayerPrefs.SetInt("NowEquipEndless1", i);
				e1.SetItem(allEquipments.list[i]);
			}
			else if (selectEquip == 2)
			{
				PlayerPrefs.SetInt("NowEquipEndless2", i);
				e2.SetItem(allEquipments.list[i]);
			}
			else if (selectEquip == 3)
			{
				PlayerPrefs.SetInt("NowEquipEndless3", i);
				e3.SetItem(allEquipments.list[i]);
			}
			SoundManager.instance.PlaySe("button");
			ChangeState(1);
		}

		public void PlayButtonSe()
		{
			if (Time.time > 0.5f)
				SoundManager.instance.PlaySe("button");
		}

		private void ChangeCanvas(int s)
		{
			for (int i = 0; i < canvasGroups.Count; i++)
			{
				if (i == s)
				{
					canvasGroups[i].alpha = 1;
					canvasGroups[i].interactable = true;
					canvasGroups[i].blocksRaycasts = true;
				}
				else
				{
					canvasGroups[i].alpha = 0;
					canvasGroups[i].interactable = false;
					canvasGroups[i].blocksRaycasts = false;
				}
			}
		}

		private void ChangeState(int s)
		{
			state = s;
			ChangeCanvas(s);
		}

		private void InitPlayerPrefs()
		{
			PlayerPrefs.SetInt("IsInitPlayerPrefs", 1);
			//キーバインドの初期化
			foreach (Transform child in keyBindElements.transform)
			{
				if (child.GetComponent<RebindUI>() != null)
				{
					child.GetComponent<RebindUI>().ResetOverrides();
				}
			}

			PlayerPrefs.SetFloat("BgmVolume", 1.0f);
			PlayerPrefs.SetFloat("SeVolume", 1.0f);
			PlayerPrefs.SetInt("RandomSeed", 1);
			PlayerPrefs.SetInt("Seed", 0);
			PlayerPrefs.SetString("SeedText", "");

			UnityroomApiClient.Instance.SendScore(2, 0, ScoreboardWriteMode.Always);
			PlayerPrefs.SetInt("NowEquipEndless1", 1);
			PlayerPrefs.SetInt("NowEquipEndless2", 0);
			PlayerPrefs.SetInt("NowEquipEndless3", 0);
			for (int i = 0; i < allEquipments.list.Count; i++)
			{
				PlayerPrefs.SetInt("Equip" + i, 0);
			}
			PlayerPrefs.SetInt("Equip0", 1);
			PlayerPrefs.SetInt("Equip1", 1);

			e1.SetItem(allEquipments.list[PlayerPrefs.GetInt("NowEquipEndless1", 0)]);
			e2.SetItem(allEquipments.list[PlayerPrefs.GetInt("NowEquipEndless2", 1)]);
			e3.SetItem(allEquipments.list[PlayerPrefs.GetInt("NowEquipEndless3", 1)]);

			Debug.Log("Init PlayerPrefs");

			if (Application.isEditor)
			{
				PlayerPrefs.SetInt("Coin", 10000);
			}
			else
			{
				PlayerPrefs.SetInt("Coin", 0);
			}

			randomSeedToggle.isOn = PlayerPrefs.GetInt("RandomSeed", 1) == 0;
			seedInputField.text = PlayerPrefs.GetString("SeedText", "");
		}

		public void ResetSetting()
		{
			foreach (Transform child in keyBindElements.transform)
			{
				if (child.GetComponent<RebindUI>() != null)
				{
					child.GetComponent<RebindUI>().ResetOverrides();
				}
			}

			PlayerPrefs.SetFloat("BgmVolume", 1.0f);
			PlayerPrefs.SetFloat("SeVolume", 1.0f);
			PlayerPrefs.SetInt("RandomSeed", 1);
			PlayerPrefs.SetInt("Seed", 0);
			PlayerPrefs.SetString("SeedText", "");

			Debug.Log("Reset PlayerPrefs");

			randomSeedToggle.isOn = PlayerPrefs.GetInt("RandomSeed", 1) == 0;
			seedInputField.text = PlayerPrefs.GetString("SeedText", "");
			bgmSlider.value = PlayerPrefs.GetFloat("BgmVolume", 1.0f);
			seSlider.value = PlayerPrefs.GetFloat("SeVolume", 1.0f);
		}

		void Awake()
		{
			Time.timeScale = 1;
			allEquipments.Init();

			CloseCredit();
			CloseLicense();
		}
		void Start()
		{
			if (PlayerPrefs.HasKey("NowEquipEndless1") == false)
			{
				InitPlayerPrefs();
			}
			bgmSlider.value = PlayerPrefs.GetFloat("BgmVolume", 1.0f);
			seSlider.value = PlayerPrefs.GetFloat("SeVolume", 1.0f);


			bgmSlider.onValueChanged.AddListener((value) =>
			{
				BGMManager.instance.BgmVolume = value;
			});

			seSlider.onValueChanged.AddListener((value) =>
			{
				SoundManager.instance.SeVolume = value;
			});
			var trigger = seSlider.gameObject.AddComponent<EventTrigger>();
			var entry = new EventTrigger.Entry();
			entry.eventID = EventTriggerType.PointerUp;
			entry.callback.AddListener(new UnityEngine.Events.UnityAction<BaseEventData>((data) =>
			{
				SoundManager.instance.PlaySe("button");
			}));
			trigger.triggers.Add(entry);

			e1.SetItem(allEquipments.list[PlayerPrefs.GetInt("NowEquipEndless1", 0)]);
			e2.SetItem(allEquipments.list[PlayerPrefs.GetInt("NowEquipEndless2", 1)]);
			e3.SetItem(allEquipments.list[PlayerPrefs.GetInt("NowEquipEndless3", 1)]);
			ChangeState(0);

			randomSeedToggle.isOn = PlayerPrefs.GetInt("RandomSeed", 1) == 0;
			if (randomSeedToggle.isOn)
			{
				seedInputField.text = PlayerPrefs.GetString("SeedText", "");
			}
		}

		void Update()
		{
			if (!randomSeedToggle.isOn)
			{
				seedInputField.interactable = false;
				seedText.color = new Color(0.2f, 0.2f, 0.2f);
				placeHolder.text = "";
				PlayerPrefs.SetInt("RandomSeed", 1);
			}
			else
			{
				seedInputField.interactable = true;
				seedText.color = Color.white;
				placeHolder.text = "Enter seed...";
				PlayerPrefs.SetInt("RandomSeed", 0);
				int seed = seedInputField.text.GetHashCode();
				PlayerPrefs.SetInt("Seed", seed);
				PlayerPrefs.SetString("SeedText", seedInputField.text);
			}
		}
	}
}
