namespace NTitle
{
	using System.Collections;
	using System.Collections.Generic;
	using UnityEngine;
	using UnityEngine.UI;
	using unityroom.Api;
	using TMPro;
	using NEquipment;
	using NManager;

	public class TitleEquipmentContainer : MonoBehaviour
	{
		private Vector3 windowOffset = new Vector3(100, -45, 0);
		private EquipmentData equipmentData;
		private Image icon => transform.Find("Icon").GetComponent<Image>();
		private TextMeshProUGUI nameText => transform.Find("Name").GetComponent<TextMeshProUGUI>();
		private Image descriptionBG => transform.Find("DescriptionBG").GetComponent<Image>();
		private TextMeshProUGUI descriptionText => descriptionBG.gameObject.transform.GetChild(0).GetComponent<TextMeshProUGUI>();
		private TextMeshProUGUI priceText => transform.Find("Price").GetComponent<TextMeshProUGUI>();
		private bool isBought = false;

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
			priceText.text = ":" + equipmentData.equipmentPrice.ToString();
			isBought = PlayerPrefs.GetInt("Equip" + equipmentData.equipmentID, 0) == 1;
			priceText.gameObject.SetActive(!isBought);

		}

		public void BuyItem()
		{
			if (PlayerPrefs.GetInt("Coin", 0) >= equipmentData.equipmentPrice)
			{
				int coin = PlayerPrefs.GetInt("Coin", 0);
				PlayerPrefs.SetInt("Coin", coin - equipmentData.equipmentPrice);
				PlayerPrefs.SetInt("Equip" + equipmentData.equipmentID, 1);
				UnityroomApiClient.Instance.SendScore(2, coin - equipmentData.equipmentPrice, ScoreboardWriteMode.Always);
				isBought = true;
				priceText.gameObject.SetActive(false);
				SoundManager.instance.PlaySe("shop");
			}
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

			isBought = PlayerPrefs.GetInt("Equip" + equipmentData?.equipmentID, 0) == 1;
			priceText.gameObject.SetActive(!isBought);
		}
	}
}
