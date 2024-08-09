namespace NBless
{
	using System.Collections;
	using System.Collections.Generic;
	using UnityEngine;
	using System;

	[CreateAssetMenu(fileName = "BlessDataList", menuName = "Scriptable Objects/BlessDataList")]
	public class BlessDataList : ScriptableObject
	{
		[SerializeField]
		public List<BlessData> list = new List<BlessData>();

		public void Reset()
		{
			list = new List<BlessData>();
		}

		public void Init()
		{
			for (int i = 0; i < list.Count; i++)
			{
				list[i].blessID = i;
			}
		}
	}
}
