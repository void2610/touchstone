namespace NUI
{
	using System.Collections;
	using System.Collections.Generic;
	using UnityEngine;
	using UnityEngine.UI;
	using TMPro;
	using NEquipment;

	public class EquipmentContainer : MonoBehaviour
	{
		public Image gauge { get; private set; }
		private EquipmentData equipmentData;
		private Image icon;
		private TextMeshProUGUI nameText;

		public void SetItem(EquipmentData e)
		{
			icon = transform.Find("Icon").GetComponent<Image>();
			nameText = transform.Find("Name").GetComponent<TextMeshProUGUI>();

			equipmentData = e;
			icon.sprite = e.equipmentIcon;
			nameText.text = e.equipmentName;
		}

		private void Awake()
		{
			icon = transform.Find("Icon").GetComponent<Image>();
			nameText = transform.Find("Name").GetComponent<TextMeshProUGUI>();
			gauge = transform.Find("Gauge").gameObject.GetComponent<Image>();
			gauge.fillAmount = 0;
		}
	}
}
