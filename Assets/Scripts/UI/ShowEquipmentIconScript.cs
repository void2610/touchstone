using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ShowEquipmentIconScript : MonoBehaviour
{
	private string weapon;
	private string skill;
	private string utility;

	private Image weaponIcon;
	private Image skillIcon;
	private Image utilityIcon;

	void Start()
	{
		weapon = PlayerPrefs.GetString("NowEquipWeapon", "Sword");
		skill = PlayerPrefs.GetString("NowEquipSkill", "Guard");
		utility = PlayerPrefs.GetString("NowEquipUtility", "Dash");

		weaponIcon = GameObject.Find("WeaponIcon").GetComponent<Image>();
		// skillIcon = GameObject.Find("SkillIcon").GetComponent<Image>();
		utilityIcon = GameObject.Find("UtilityIcon").GetComponent<Image>();

		weaponIcon.GetComponent<Image>().sprite = Resources.Load<Sprite>("Pictures/Equipment/Weapon/" + weapon);
		// skillIcon.GetComponent<Image>().sprite = Resources.Load<Sprite>("Pictures/Equipment/Skill/" + skill);
		utilityIcon.GetComponent<Image>().sprite = Resources.Load<Sprite>("Pictures/Equipment/Utility/" + utility);
	}
}
