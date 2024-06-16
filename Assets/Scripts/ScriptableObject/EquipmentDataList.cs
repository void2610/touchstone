namespace NEquipment
{
	using System.Collections;
	using System.Collections.Generic;
	using UnityEngine;
	using System;

	[CreateAssetMenu(fileName = "EquipmentDataList", menuName = "Scriptable Objects/EquipmentDataList")]
	public class EquipmentDataList : ScriptableObject
	{
		[SerializeField]
		public List<EquipmentData> list = new List<EquipmentData>();

		public void Reset()
		{
			list = new List<EquipmentData>();
		}
	}
}