namespace NTitle
{
	using System.Collections;
	using System.Collections.Generic;
	using UnityEngine;
	using UnityEngine.UI;


	public class EquipmentContainer : MonoBehaviour
	{
		private Image icon;
		private Text nameText;

		public void SetItem(string n, Sprite s)
		{
			icon.sprite = s;
			nameText.text = n;
		}

		private void Start()
		{
			icon = transform.Find("Icon").GetComponent<Image>();
			nameText = transform.Find("Name").GetComponent<Text>();
		}
	}
}
