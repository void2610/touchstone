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
		private AudioClip bgmAudioClip;

		private int state = 0;
		//0:タイトル画面 1:装備編成画面 2:設定画面 3:装備選択画面
		private int selectEquip = 0;

		public void OnClickStartButton()
		{
			SoundManager.instance.PlaySe("button");
			SoundManager.instance.StopBgm();
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

		public void OnClickMainEntryButton()
		{
			//ゲームシーンへ
			SoundManager.instance.PlaySe("button");
			SoundManager.instance.StopBgm();
			Initiate.Fade("MainScene", Color.black, 1.0f);
		}

		public void OnClickEndlessEntryButton()
		{
			//ゲームシーンへ
			SoundManager.instance.PlaySe("button");
			SoundManager.instance.StopBgm();
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
				PlayerPrefs.SetInt("NowEquip1", i);
				e1.SetItem(allEquipments.list[i]);
			}
			else if (selectEquip == 2)
			{
				PlayerPrefs.SetInt("NowEquip2", i);
				e2.SetItem(allEquipments.list[i]);
			}
			else if (selectEquip == 3)
			{
				PlayerPrefs.SetInt("NowEquip3", i);
				e3.SetItem(allEquipments.list[i]);
			}
			SoundManager.instance.PlaySe("button");
			ChangeState(1);
		}

		public void ResetPlayerPrefs()
		{
			SoundManager.instance.PlaySe("button");
			PlayerPrefs.DeleteAll();
			Debug.Log("DeleteAll");
			InitPlayerPrefs();
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
			PlayerPrefs.SetFloat("BgmVolume", 0.5f);
			PlayerPrefs.SetFloat("SeVolume", 0.5f);

			UnityroomApiClient.Instance.SendScore(2, 0, ScoreboardWriteMode.Always);
			PlayerPrefs.SetInt("NowEquip1", 0);
			PlayerPrefs.SetInt("NowEquip2", 1);
			PlayerPrefs.SetInt("NowEquip3", 1);
			for (int i = 0; i < allEquipments.list.Count; i++)
			{
				PlayerPrefs.SetInt("Equip" + i, 0);
			}
			PlayerPrefs.SetInt("Equip0", 1);
			PlayerPrefs.SetInt("Equip1", 1);

			e1.SetItem(allEquipments.list[PlayerPrefs.GetInt("NowEquip1", 0)]);
			e2.SetItem(allEquipments.list[PlayerPrefs.GetInt("NowEquip2", 1)]);
			e3.SetItem(allEquipments.list[PlayerPrefs.GetInt("NowEquip3", 1)]);
			Debug.Log("Init PlayerPrefs");

			if (Application.isEditor)
			{
				PlayerPrefs.SetInt("Coin", 10000);
			}
			else
			{
				PlayerPrefs.SetInt("Coin", 0);
			}
		}

		void Awake()
		{
			Time.timeScale = 1;
			allEquipments.Init();
			if (PlayerPrefs.HasKey("NowEquip1") == false)
			{
				InitPlayerPrefs();
			}
			bgmSlider.value = PlayerPrefs.GetFloat("BgmVolume", 0.5f);
			seSlider.value = PlayerPrefs.GetFloat("SeVolume", 0.5f);
		}
		void Start()
		{
			bgmSlider.onValueChanged.AddListener((value) =>
			{
				SoundManager.instance.BgmVolume = value;
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

			e1.SetItem(allEquipments.list[PlayerPrefs.GetInt("NowEquip1", 0)]);
			e2.SetItem(allEquipments.list[PlayerPrefs.GetInt("NowEquip2", 1)]);
			e3.SetItem(allEquipments.list[PlayerPrefs.GetInt("NowEquip3", 1)]);
			ChangeState(0);
			SoundManager.instance.PlayBgm(bgmAudioClip);
		}
	}
}