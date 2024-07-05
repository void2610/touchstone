namespace NManager
{
	using System;
	using System.Collections;
	using System.Collections.Generic;
	using UnityEngine;
	using NEquipment;
	using NUI;
	using NInterface;

	public class EquipmentManager : MonoBehaviour
	{
		[SerializeField]
		private List<string> actionKeys = new List<string>() { "Fire1", "Fire2", "Fire3" };
		[SerializeField]
		private List<EquipmentContainer> equipmentContainers = new List<EquipmentContainer>();
		private List<EquipmentData> equipmentList = new List<EquipmentData>();
		private List<GameObject> equipmentObjList = new List<GameObject>();
		private int n = 3;

		public void ChangeAllEquipmentEnabled(bool enabled)
		{
			foreach (GameObject g in equipmentObjList)
			{
				g.GetComponent<Equipment>().Disable();
			}
		}

		private void Start()
		{
			for (int i = 0; i < n; i++)
			{
				equipmentList.Add(GameManager.instance.allEquipmentDataList.list[PlayerPrefs.GetInt("NowEquip" + (i + 1).ToString())]);
				GameObject g = Instantiate((GameObject)Resources.Load("Prefabs/Equipment/" + equipmentList[i].equipmentName));
				g.GetComponent<Equipment>().Init(GameManager.instance.playerObj, equipmentContainers[i].gauge, actionKeys[i]);
				equipmentObjList.Add(g);
				equipmentContainers[i].SetItem(equipmentList[i]);
			}

			for (int i = 0; i < n; i++)
			{
				if (equipmentObjList[i].GetComponent<IEquipmentModifiable>() != null)
				{
					equipmentObjList[i].GetComponent<IEquipmentModifiable>().ModifyEquipment(equipmentObjList[0].GetComponent<Equipment>(), equipmentObjList[1].GetComponent<Equipment>(), equipmentObjList[2].GetComponent<Equipment>());
				}
			}
		}
	}
}
