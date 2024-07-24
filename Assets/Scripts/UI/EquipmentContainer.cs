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
		private Image icon => transform.Find("Icon").GetComponent<Image>();
		private TextMeshProUGUI nameText => transform.Find("Name").GetComponent<TextMeshProUGUI>();

		public void SetItem(EquipmentData e)
		{
			equipmentData = e;
			icon.sprite = e.equipmentIcon;
			nameText.text = e.equipmentName;
		}

		public void SetGauge(float value)
		{
			if (gauge != null)
			{
				gauge.fillAmount = value;
			}
		}

		private void Awake()
		{
			if (transform.Find("Gauge") != null)
			{
				gauge = transform.Find("Gauge").gameObject.GetComponent<Image>();
				gauge.fillAmount = 0;
			}
		}
	}
}
