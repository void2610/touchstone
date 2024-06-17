namespace NManager
{
	using System;
	using System.Collections;
	using System.Collections.Generic;
	using UnityEngine;
	using NEquipment;
	using NUI;

	public class EquipmentManager : MonoBehaviour
	{
		[SerializeField]
		private List<EquipmentContainer> equipmentContainers = new List<EquipmentContainer>();
		private List<EquipmentData> equipmentList = new List<EquipmentData>();
		private List<GameObject> equipmentObjList = new List<GameObject>();
		private int n = 3;
		void Start()
		{
			for (int i = 0; i < n; i++)
			{
				equipmentList.Add(GameManager.instance.allEquipmentDataList.list[PlayerPrefs.GetInt("NowEquip" + (i + 1).ToString())]);
				GameObject g = Instantiate((GameObject)Resources.Load("Prefabs/Equipment/" + equipmentList[i].equipmentName));
				g.GetComponent<Skill>().Init(equipmentContainers[i].gauge);
				equipmentObjList.Add(g);
				equipmentObjList[i].GetComponent<Equipment>().actionKey = "Fire" + (i + 1).ToString();
				equipmentContainers[i].SetItem(equipmentList[i]);
			}
		}
	}
}
