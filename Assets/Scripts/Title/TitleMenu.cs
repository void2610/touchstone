namespace NTitle
{
	using System.Collections;
	using System.Collections.Generic;
	using UnityEngine;
	using UnityEngine.SceneManagement;
	using UnityEngine.UI;
	using NEquipment;
	using TMPro;

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

		private int state = 0;
		//0:タイトル画面 1:装備編成画面 2:設定画面 3:装備選択画面
		private int selectEquip = 0;

		public void OnClickStartButton()
		{
			ChangeState(1);
		}

		public void OnClickSettingButton()
		{
			ChangeState(2);
		}

		public void OnClickBackButton()
		{
			ChangeState(0);
		}

		public void OnClickQuitButton()
		{
			//ゲーム終了
			Application.Quit();
			//エディターでプレイモードを終了
			// UnityEditor.EditorApplication.isPlaying = false;
		}

		public void OnClickEntryButton()
		{
			//ゲームシーンへ
			SceneManager.LoadScene("SampleScene");
			PlayerPrefs.SetInt("PlayerHp", 10);
			PlayerPrefs.SetInt("PlayerMaxHp", 10);
		}

		public void OnClickEquip1Button()
		{
			selectEquip = 1;
			ChangeState(3);
		}
		public void OnClickEquip2Button()
		{
			selectEquip = 2;
			ChangeState(3);
		}
		public void OnClickEquip3Button()
		{
			selectEquip = 3;
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
			ChangeState(1);
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
			PlayerPrefs.SetInt("Coin", 0);
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
		}

		void Awake()
		{
			allEquipments.Init();
			if (PlayerPrefs.HasKey("NowEquip1") == false)
			{
				InitPlayerPrefs();
			}
		}
		void Start()
		{
			e1.SetItem(allEquipments.list[PlayerPrefs.GetInt("NowEquip1", 0)]);
			e2.SetItem(allEquipments.list[PlayerPrefs.GetInt("NowEquip2", 1)]);
			e3.SetItem(allEquipments.list[PlayerPrefs.GetInt("NowEquip3", 1)]);
			ChangeState(0);
		}

		void Update()
		{
			if (Input.GetKeyDown(KeyCode.N))
			{
				PlayerPrefs.DeleteAll();
				Debug.Log("DeleteAll");
				InitPlayerPrefs();
			}
		}
	}
}
