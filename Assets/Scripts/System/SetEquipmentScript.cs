using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using NEquipment;

public class SetEquipmentScript : MonoBehaviour
{
	private string weapon = "Sword";
	private string skill1 = "Grapple";
	private string skill2 = "Dash";
	private GameObject weaponObj = null;
	private GameObject skill1Obj = null;
	private GameObject skill2Obj = null;
	void Start()
	{
		weapon = PlayerPrefs.GetString("NowEquipWeapon", "Sword");
		skill1 = PlayerPrefs.GetString("NowEquipSkill1", "Grapple");
		skill2 = PlayerPrefs.GetString("NowEquipSkill2", "Dash");

		weaponObj = GameObject.Instantiate((GameObject)Resources.Load("Prefabs/Equipment/Weapon/" + weapon));
		skill1Obj = GameObject.Instantiate((GameObject)Resources.Load("Prefabs/Equipment/Skill/" + skill1));
		skill2Obj = GameObject.Instantiate((GameObject)Resources.Load("Prefabs/Equipment/Skill/" + skill2));

		skill1Obj.GetComponent<Skill>().actionKey = "Fire2";
		skill2Obj.GetComponent<Skill>().actionKey = "Fire3";
	}
}
