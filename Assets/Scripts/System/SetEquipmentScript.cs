using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SetEquipmentScript : MonoBehaviour
{
	private string weapon = "Sword";
	private string skill1 = "Grapple";
	private string skill2 = "Dash";
	void Start()
	{
		weapon = PlayerPrefs.GetString("NowEquipWeapon", "Sword");
		skill1 = PlayerPrefs.GetString("NowEquipSkill1", "Grapple");
		skill2 = PlayerPrefs.GetString("NowEquipSkill2", "Dash");

		GameObject.Instantiate((GameObject)Resources.Load("Prefabs/Equipment/Weapon/" + weapon));
		GameObject.Instantiate((GameObject)Resources.Load("Prefabs/Equipment/Skill/" + skill1));
		GameObject.Instantiate((GameObject)Resources.Load("Prefabs/Equipment/Skill/" + skill2));
		Debug.Log(weapon + " " + skill1 + " " + skill2);
	}
}
