namespace NTitle
{
	using System.Collections;
	using System.Collections.Generic;
	using UnityEngine;
	using UnityEngine.UI;
	using NEquipment;

	public class TitleEquipmentContainer : MonoBehaviour
	{
		private EquipmentData equipmentData;
		private Image icon;
		private Text nameText;

		public void SwitchText(bool b)
		{
			nameText.enabled = b;
		}

		public void SetItem(EquipmentData e)
		{
			icon = transform.Find("Icon").GetComponent<Image>();
			nameText = transform.Find("Name").GetComponent<Text>();

			equipmentData = e;
			icon.sprite = e.equipmentIcon;
			nameText.text = e.equipmentName;
		}

		private void Start()
		{
			icon = transform.Find("Icon").GetComponent<Image>();
			nameText = transform.Find("Name").GetComponent<Text>();
		}
	}
}
