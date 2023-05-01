using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SetEquipmentScript : MonoBehaviour
{
	private string weapon = "Sword";
	private string skill = "Grapple";
	private string utility = "Dash";
	void Start()
	{
		weapon = PlayerPrefs.GetString("NowEquipWeapon", "Sword");
		skill = PlayerPrefs.GetString("NowEquipSkill", "Grapple");
		utility = PlayerPrefs.GetString("NowEquipUtility", "Dash");

		GameObject.Instantiate((GameObject)Resources.Load("Prefabs/Equipment/Weapon/" + weapon));
		GameObject.Instantiate((GameObject)Resources.Load("Prefabs/Equipment/Skill/" + skill));
		GameObject.Instantiate((GameObject)Resources.Load("Prefabs/Equipment/Utility/" + utility));
		Debug.Log(weapon + " " + skill + " " + utility);
	}
}
