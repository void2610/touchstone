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

		private void AdjustWidth()
		{
			//改行コードを全て削除
			textComponent.text = textComponent.text.Replace("\n", "");

			string[] words = textComponent.text.Split(' ');
			string wrappedText = "";

			float textWidth = maxWidth;
			float spaceWidth = textComponent.fontSize;

			float currentLineWidth = 0;

			foreach (string word in words)
			{
				float wordWidth = word.Length * textComponent.fontSize;

				if (currentLineWidth + wordWidth <= textWidth)
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
			AdjustHeight();
		}

		private void AdjustHeight()
		{
			float originalFontSize = textComponent.fontSize;
			string[] lines = textComponent.text.Split('\n');
			float height = lines.Length * originalFontSize;
			Debug.Log(height);
			if (height > maxHeight)
			{
				textComponent.fontSize = (int)(originalFontSize * maxHeight / height);
				AdjustWidth();
			}
		}

		void Start()
		{
			textComponent = this.GetComponent<Text>();
			AdjustWidth();
		}


	}
}