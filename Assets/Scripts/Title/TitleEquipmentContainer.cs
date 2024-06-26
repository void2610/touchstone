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
		[SerializeField]
		private Vector3 offset;
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
			descriptionText = transform.Find("Description").GetComponent<TextMeshProUGUI>();
			descriptionText.enabled = false;
			descriptionBG = descriptionText.gameObject.transform.GetChild(0).GetComponent<Image>();
			descriptionBG.enabled = false;
		}

		private void Update()
		{
			descriptionText.transform.position = Vector3.Lerp(descriptionText.transform.position, Input.mousePosition + offset, Time.deltaTime * 10);
		}
	}
}
