namespace NUI
{
	using System.Collections;
	using System.Collections.Generic;
	using UnityEngine;
	using UnityEngine.UI;
	public class AdjustTextSizeScript : MonoBehaviour
	{
		[SerializeField]
		private float maxWidth;
		[SerializeField]
		private float maxHeight;

		private Text textComponent;
		private string tmp;

		private void AdjustText()
		{
			float originalFontSize = textComponent.fontSize;
			string[] lines = textComponent.text.Split('\n');
			float height = lines.Length * originalFontSize;
			if (height > maxHeight)
			{
				textComponent.fontSize = (int)(originalFontSize * maxHeight / height);
			}
		}

		void Start()
		{
			textComponent = this.GetComponent<Text>();
			tmp = "aaa";
		}

		void Update()
		{
			if (tmp != textComponent.text)
			{
				AdjustText();
				tmp = textComponent.text;
			}

			tmp = textComponent.text;
		}
	}
}