namespace NMenu
{
	using System.Collections;
	using System.Collections.Generic;
	using UnityEngine;
	using UnityEngine.SceneManagement;
	using UnityEngine.UI;
	using NArtifact;
	using TMPro;

	public class TitleMenu : MonoBehaviour
	{
		[SerializeField]
		private ArtifactList obtainedArtifacts;

		[SerializeField]
		private List<CanvasGroup> canvasGroups = new List<CanvasGroup>();
		[SerializeField]
		private GameObject nowWeapon;
		[SerializeField]
		private GameObject nowSkill;
		[SerializeField]
		private GameObject nowUtility;

		private int state = 0;
		//0:タイトル画面 1:装備編成画面 2:設定画面 3:weapon 4:skill1 5:skill2

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
			UnityEditor.EditorApplication.isPlaying = false;
		}

		public void OnClickEntryButton()
		{
			//ゲームシーンへ
			SceneManager.LoadScene("SampleScene");
			PlayerPrefs.SetInt("PlayerHp", 10);
			PlayerPrefs.SetInt("PlayerMaxHp", 10);
			obtainedArtifacts.ResetToInitial();
		}

		public void OnClickWeaponButton()
		{
			ChangeState(3);
		}

		public void OnClickSkillButton()
		{
			ChangeState(4);
		}

		public void OnClickUtilityButton()
		{
			ChangeState(5);
		}

		public void OnClickSetWeaponButton(Button b)
		{
			PlayerPrefs.SetString("NowEquipWeapon", b.name);
			nowWeapon.GetComponent<Image>().sprite = Resources.Load<Sprite>("Pictures/Equipment/Weapon/" + b.name);
			ChangeState(1);
		}

		public void OnClickSetSkill1Button(Button b)
		{
			PlayerPrefs.SetString("NowEquipSkill1", b.name);
			nowSkill.GetComponent<Image>().sprite = Resources.Load<Sprite>("Pictures/Equipment/Skill/" + b.name);
			ChangeState(1);
		}

		public void OnClickSetSkill2Button(Button b)
		{
			Debug.Log(b.name);
			PlayerPrefs.SetString("NowEquipSkill2", b.name);
			nowUtility.GetComponent<Image>().sprite = Resources.Load<Sprite>("Pictures/Equipment/Skill/" + b.name);
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

		void Start()
		{
			PlayerPrefs.SetString("NowEquipWeapon", "Sword");
			PlayerPrefs.SetString("NowEquipSkill1", "Grapple");
			PlayerPrefs.SetString("NowEquipSkill2", "Dash");

			nowWeapon.GetComponent<Image>().sprite = Resources.Load<Sprite>("Pictures/Equipment/Weapon/" + PlayerPrefs.GetString("NowEquipWeapon", "Sword"));
			nowSkill.GetComponent<Image>().sprite = Resources.Load<Sprite>("Pictures/Equipment/Skill/" + PlayerPrefs.GetString("NowEquipSkill1", "Grapple"));
			nowUtility.GetComponent<Image>().sprite = Resources.Load<Sprite>("Pictures/Equipment/Skill/" + PlayerPrefs.GetString("NowEquipUSkill2", "Dash"));

			ChangeState(0);
		}
	}
}
