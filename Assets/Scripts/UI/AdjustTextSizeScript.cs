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
		[SerializeField]
		private bool canBreakLine = true;

		private Text textComponent;
		private string tmp;

		private void AdjustWidth()
		{
			//改行コードを全て削除
			textComponent.text = textComponent.text.Replace("\n", "");

			string[] words = textComponent.text.Split(' ');

			string wrappedText = "";

			float spaceWidth = textComponent.fontSize;

			float currentLineWidth = 0;

			foreach (string word in words)
			{
				float wordWidth = word.Length * textComponent.fontSize;

				if (currentLineWidth + wordWidth <= maxWidth)
				{
					wrappedText += word + " ";
					currentLineWidth += wordWidth + spaceWidth;
				}
				else
				{
					wrappedText += "\n" + word + " ";
					currentLineWidth = wordWidth + spaceWidth;
				}
			}

			textComponent.text = wrappedText;
		}

		private void AdjustHeight()
		{
			float originalFontSize = textComponent.fontSize;
			string[] lines = textComponent.text.Split('\n');
			float height = lines.Length * originalFontSize;
			if (height > maxHeight)
			{
				textComponent.fontSize = (int)(originalFontSize * maxHeight / height);
				if (canBreakLine)
				{
					AdjustWidth();
				}
			}
		}

		private void AdjustText()
		{
			if (canBreakLine)
			{
				AdjustWidth();
			}
			AdjustHeight();
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
				Debug.Log(tmp + " -> " + textComponent.text);
				AdjustText();
				tmp = textComponent.text;
			}

			tmp = textComponent.text;
		}

	}
}