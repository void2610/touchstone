namespace NManager
{
	using System;
	using System.Collections;
	using System.Collections.Generic;
	using UnityEngine;
	using UnityEngine.InputSystem;
	using NEquipment;
	using NUI;
	using NInterface;

	public class EquipmentManager : MonoBehaviour
	{
		[SerializeField]
		private bool isEndless = false;
		[SerializeField]
		private List<InputAction> actions = new List<InputAction>();
		[SerializeField]
		private List<EquipmentContainer> equipmentContainers = new List<EquipmentContainer>();
		private EquipmentData[] equipmentList = { null, null, null };
		private GameObject[] equipmentObjList = { null, null, null };
		private int n = 3;
		private PlayerInput playerInput => this.GetComponent<PlayerInput>();

		public void ChangeAllEquipmentEnabled(bool enabled)
		{
			if (enabled)
			{
				foreach (GameObject g in equipmentObjList)
				{
					g.GetComponent<Equipment>().Enable();
				}
			}
			else
			{
				foreach (GameObject g in equipmentObjList)
				{
					g.GetComponent<Equipment>().Disable();
				}
			}
		}

		public void SetUp()
		{
			for (int i = 0; i < n; i++)
			{
				if (equipmentObjList[i] != null)
				{
					Destroy(equipmentObjList[i]);
					equipmentList[i] = null;
					equipmentObjList[i] = null;
				}
			}

			for (int i = 0; i < n; i++)
			{
				if (isEndless)
				{
					equipmentList[i] = (GameManager.instance.allEquipmentDataList.list[PlayerPrefs.GetInt("NowEquipEndless" + (i + 1).ToString())]);
				}
				else
				{
					equipmentList[i] = (GameManager.instance.allEquipmentDataList.list[PlayerPrefs.GetInt("NowEquip" + (i + 1).ToString())]);
				}

				GameObject g = Instantiate((GameObject)Resources.Load("Prefabs/Equipment/" + equipmentList[i].equipmentName));
				g.GetComponent<Equipment>().Init(GameManager.instance.playerObj, equipmentContainers[i].gauge, actions[i]);
				equipmentObjList[i] = g;
				equipmentContainers[i].SetItem(equipmentList[i]);
				equipmentContainers[i].GetComponent<EquipmentContainer>().SetGauge(0);
			}

			for (int i = 0; i < n; i++)
			{
				if (equipmentObjList[i].GetComponent<IEquipmentModifiable>() != null)
				{
					equipmentObjList[i].GetComponent<IEquipmentModifiable>().ModifyEquipment(equipmentObjList[0].GetComponent<Equipment>(), equipmentObjList[1].GetComponent<Equipment>(), equipmentObjList[2].GetComponent<Equipment>());
				}
			}
		}

		private void Awake()
		{
			if (!isEndless)
			{
				PlayerPrefs.SetInt("NowEquip1", 1);
				PlayerPrefs.SetInt("NowEquip2", 0);
				PlayerPrefs.SetInt("NowEquip3", 0);
			}

			actions.Add(playerInput.actions["Equip1"]);
			actions.Add(playerInput.actions["Equip2"]);
			actions.Add(playerInput.actions["Equip3"]);
		}
	}
}
