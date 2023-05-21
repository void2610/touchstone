namespace NMenu
{


	using System.Collections;
	using System.Collections.Generic;
	using UnityEngine;
	using UnityEngine.SceneManagement;
	using UnityEngine.UI;
	using NArtifact;

	public class TitleMenuScript : MonoBehaviour
	{
		[SerializeField]
		private ArtifactList obtainedArtifacts;

		[SerializeField]
		private GameObject titleText;
		[SerializeField]
		private GameObject startButton;
		[SerializeField]
		private GameObject settingButton;
		[SerializeField]
		private GameObject backButton;
		[SerializeField]
		private GameObject quitButton;
		[SerializeField]
		private GameObject entryButton;
		[SerializeField]
		private GameObject equipments;
		[SerializeField]
		private GameObject weapons;
		[SerializeField]
		private GameObject skills1;
		[SerializeField]
		private GameObject skills2;
		[SerializeField]
		private GameObject nowWeapon;
		[SerializeField]
		private GameObject nowSkill;
		[SerializeField]
		private GameObject nowUtility;

		private string weaponName = "Sword";
		private string skillName = "Dash";
		private string utilityName = "Grapple";

		private int state = 0;
		//0:タイトル画面 1:装備編成画面 2:設定画面 3:weapon 4:skill1 5:skill2

		public void OnClickStartButton()
		{
			state = 1;
		}

		public void OnClickSettingButton()
		{
			state = 2;
		}

		public void OnClickBackButton()
		{
			if (state == 3 || state == 4 || state == 5)
			{
				state = 1;
			}
			else
			{
				state = 0;
			}
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
			state = 3;
		}

		public void OnClickSkillButton()
		{
			state = 4;
		}

		public void OnClickUtilityButton()
		{
			state = 5;
		}

		public void OnClickSetWeaponButton(Button b)
		{
			PlayerPrefs.SetString("NowEquipWeapon", b.name);
			nowWeapon.GetComponent<Image>().sprite = Resources.Load<Sprite>("Pictures/Equipment/Weapon/" + b.name);
			state = 1;
		}

		public void OnClickSetSkill1Button(Button b)
		{
			PlayerPrefs.SetString("NowEquipSkill1", b.name);
			nowSkill.GetComponent<Image>().sprite = Resources.Load<Sprite>("Pictures/Equipment/Skill/" + b.name);
			state = 1;
		}

		public void OnClickSetSkill2Button(Button b)
		{
			Debug.Log(b.name);
			PlayerPrefs.SetString("NowEquipSkill2", b.name);
			nowUtility.GetComponent<Image>().sprite = Resources.Load<Sprite>("Pictures/Equipment/Skill/" + b.name);
			state = 1;
		}

		void Start()
		{
			PlayerPrefs.SetString("NowEquipWeapon", "Sword");
			PlayerPrefs.SetString("NowEquipSkill1", "Grapple");
			PlayerPrefs.SetString("NowEquipSkill2", "Dash");

			nowWeapon.GetComponent<Image>().sprite = Resources.Load<Sprite>("Pictures/Equipment/Weapon/" + PlayerPrefs.GetString("NowEquipWeapon", "Sword"));
			nowSkill.GetComponent<Image>().sprite = Resources.Load<Sprite>("Pictures/Equipment/Skill/" + PlayerPrefs.GetString("NowEquipSkill1", "Grapple"));
			nowUtility.GetComponent<Image>().sprite = Resources.Load<Sprite>("Pictures/Equipment/Skill/" + PlayerPrefs.GetString("NowEquipUSkill2", "Dash"));
		}

		void Update()
		{
			if (state == 0)
			{
				titleText.GetComponent<Text>().text = "Tower of the Dead";
				startButton.SetActive(true);
				settingButton.SetActive(true);
				backButton.SetActive(false);
				quitButton.SetActive(true);
				entryButton.SetActive(false);
				equipments.SetActive(false);
				weapons.SetActive(false);
				skills1.SetActive(false);
				skills2.SetActive(false);
			}
			else if (state == 1)
			{
				titleText.GetComponent<Text>().text = "Equipment organization";
				startButton.SetActive(false);
				settingButton.SetActive(false);
				backButton.SetActive(true);
				quitButton.SetActive(false);
				entryButton.SetActive(true);
				equipments.SetActive(true);
				weapons.SetActive(false);
				skills1.SetActive(false);
				skills2.SetActive(false);
			}
			else if (state == 2)
			{
				titleText.GetComponent<Text>().text = "Setting";
				startButton.SetActive(false);
				settingButton.SetActive(false);
				backButton.SetActive(true);
				quitButton.SetActive(false);
				entryButton.SetActive(false);
				equipments.SetActive(false);
				weapons.SetActive(false);
				skills1.SetActive(false);
				skills2.SetActive(false);
			}
			else if (state == 3)
			{
				titleText.GetComponent<Text>().text = "Weapon";
				startButton.SetActive(false);
				settingButton.SetActive(false);
				backButton.SetActive(true);
				quitButton.SetActive(false);
				entryButton.SetActive(true);
				equipments.SetActive(false);
				weapons.SetActive(true);
				skills1.SetActive(false);
				skills2.SetActive(false);
			}
			else if (state == 4)
			{
				titleText.GetComponent<Text>().text = "Skill";
				startButton.SetActive(false);
				settingButton.SetActive(false);
				backButton.SetActive(true);
				quitButton.SetActive(false);
				entryButton.SetActive(true);
				equipments.SetActive(false);
				weapons.SetActive(false);
				skills1.SetActive(true);
				skills2.SetActive(false);
			}
			else if (state == 5)
			{
				titleText.GetComponent<Text>().text = "Utility";
				startButton.SetActive(false);
				settingButton.SetActive(false);
				backButton.SetActive(true);
				quitButton.SetActive(false);
				entryButton.SetActive(true);
				equipments.SetActive(false);
				weapons.SetActive(false);
				skills1.SetActive(false);
				skills2.SetActive(true);
			}
		}
	}
}
