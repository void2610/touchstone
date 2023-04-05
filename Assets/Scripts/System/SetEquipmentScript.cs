using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SetEquipmentScript : MonoBehaviour
{
	void Start()
	{
		PlayerPrefs.SetString("NowEquipWeapon", "Sword");
		PlayerPrefs.SetString("NowEquipSkill", "Grapple");
		PlayerPrefs.SetString("NowEquipUtility", "Dash");
	}
}
