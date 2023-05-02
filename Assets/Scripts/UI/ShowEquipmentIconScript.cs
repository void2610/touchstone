namespace NUI
{
	using System.Collections;
	using System.Collections.Generic;
	using UnityEngine;
	using UnityEngine.UI;

	public class ShowEquipmentIconScript : MonoBehaviour
	{
		private string weapon;
		private string skill1;
		private string skill2;

		private Image weaponIcon;
		private Image skill1Icon;
		private Image skill2Icon;

		void Start()
		{
			weapon = PlayerPrefs.GetString("NowEquipWeapon", "Sword");
			skill1 = PlayerPrefs.GetString("NowEquipSkill1", "Guard");
			skill2 = PlayerPrefs.GetString("NowEquipSkill2", "Dash");

			weaponIcon = GameObject.Find("WeaponIcon").GetComponent<Image>();
			skill1Icon = GameObject.Find("Skill1Icon").GetComponent<Image>();
			skill2Icon = GameObject.Find("Skill2Icon").GetComponent<Image>();

			weaponIcon.GetComponent<Image>().sprite = Resources.Load<Sprite>("Pictures/Equipment/Weapon/" + weapon);
			skill1Icon.GetComponent<Image>().sprite = Resources.Load<Sprite>("Pictures/Equipment/Skill/" + skill1);
			skill2Icon.GetComponent<Image>().sprite = Resources.Load<Sprite>("Pictures/Equipment/Skill/" + skill2);
		}
	}
}
