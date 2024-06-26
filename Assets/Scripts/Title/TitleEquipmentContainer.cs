namespace NTitle
{
	using System.Collections;
	using System.Collections.Generic;
	using UnityEngine;
	using UnityEngine.UI;
	using TMPro;
	using NEquipment;

	public class TitleEquipmentContainer : MonoBehaviour
	{
		private Vector3 windowOffset = new Vector3(100, -45, 0);
		private EquipmentData equipmentData;
		private Image icon;
		private TextMeshProUGUI nameText;
		private TextMeshProUGUI descriptionText;
		private Image descriptionBG;

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
			icon = transform.Find("Icon").GetComponent<Image>();
			nameText = transform.Find("Name").GetComponent<TextMeshProUGUI>();

			equipmentData = e;
			icon.sprite = e.equipmentIcon;
			nameText.text = e.equipmentName;
			descriptionText.text = e.equipmentDescription;
		}

		private void Awake()
		{
			icon = transform.Find("Icon").GetComponent<Image>();
			nameText = transform.Find("Name").GetComponent<TextMeshProUGUI>();
			descriptionBG = transform.Find("DescriptionBG").GetComponent<Image>();
			descriptionText = descriptionBG.gameObject.transform.GetChild(0).GetComponent<TextMeshProUGUI>();
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
