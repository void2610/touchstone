namespace NUI
{
	using System.Collections;
	using System.Collections.Generic;
	using UnityEngine;
	using UnityEngine.UI;
	using unityroom.Api;
	using TMPro;
	using NEquipment;
	using NManager;

	public class SelectEquipmentContainer : MonoBehaviour
	{
		private Vector3 windowOffset = new Vector3(100, -45, 0);
		private EquipmentData equipmentData;
		private Image icon => transform.Find("Icon").GetComponent<Image>();
		private TextMeshProUGUI nameText => transform.Find("Name").GetComponent<TextMeshProUGUI>();
		private Image descriptionBG => transform.Find("DescriptionBG").GetComponent<Image>();
		private TextMeshProUGUI descriptionText => descriptionBG.gameObject.transform.GetChild(0).GetComponent<TextMeshProUGUI>();

		public void OnPointerEnter()
		{
			descriptionText.enabled = true;
			descriptionBG.enabled = true;
		}

		public void OnPointerExit()
		{
			descriptionText.enabled = false;
			descriptionBG.enabled = false;
		}

		public void SwitchText(bool b)
		{
			nameText.enabled = b;
		}

		public void SetItem(EquipmentData e)
		{
			equipmentData = e;
			icon.sprite = equipmentData.equipmentIcon;
			nameText.text = equipmentData.equipmentName;
			descriptionText.text = equipmentData.equipmentDescription;
		}

		public EquipmentData GetItem()
		{
			return equipmentData;
		}

		private void Awake()
		{
			descriptionText.enabled = false;
			descriptionBG.enabled = false;
		}

		private void Update()
		{
			descriptionBG.transform.position = Vector3.Lerp(descriptionBG.transform.position, Input.mousePosition + windowOffset, Time.deltaTime * 10);
			descriptionBG.transform.localScale = Vector3.one / descriptionBG.transform.parent.localScale.x;
		}
	}
}
