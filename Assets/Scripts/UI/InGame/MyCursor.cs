namespace NUI
{
	using System.Collections;
	using System.Collections.Generic;
	using UnityEngine;
	using NManager;

	public class MyCursor : MonoBehaviour
	{
		[SerializeField]
		private Texture2D cursorDotTexture;
		[SerializeField]
		private Material cursorDotMaterial;
		[SerializeField]
		private float cursorDotSize = 0.3f;
		[SerializeField]
		private float cursorDotNum = 5;

		private List<GameObject> dots = new List<GameObject>();

		private float time = 0;
		private int size = 8;

		void Start()
		{
			this.transform.localScale = new Vector3(size, size, 1);
			Cursor.visible = false;

			if (cursorDotTexture != null)
			{
				for (int i = 0; i < cursorDotNum; i++)
				{
					GameObject dot = new GameObject("CursorDot" + i);
					dot.transform.parent = this.transform;
					dot.transform.localScale = new Vector3(cursorDotSize, cursorDotSize, 1);
					dot.AddComponent<SpriteRenderer>().sprite = Sprite.Create(cursorDotTexture, new Rect(0, 0, cursorDotTexture.width, cursorDotTexture.height), new Vector2(0.5f, 0.5f));
					dot.GetComponent<SpriteRenderer>().material = cursorDotMaterial;
					dots.Add(dot);
				}
			}
		}

		void Update()
		{
			float scaleAdjustment = 1080f / Screen.height;
			this.transform.localScale = new Vector3(size * scaleAdjustment * 0.6f, size * scaleAdjustment * 0.6f, 1);
			
			var target = GameManager.instance.player.transform.position;
			time += Time.deltaTime;
			// Vector3でマウス位置座標を取得する
			var cursorPos = Input.mousePosition;
			// Z軸修正
			cursorPos.z = 10f;
			cursorPos = Camera.main.ScreenToWorldPoint(cursorPos);
			// ワールド座標に変換されたマウス座標を代入
			if (GameManager.instance.state != GameManager.GameState.GameOver && GameManager.instance.state != GameManager.GameState.Other)
			{
				this.transform.position = Vector3.Lerp(this.transform.position, cursorPos, Time.deltaTime * 40f);

				if (time >= 2)
				{
					this.transform.rotation = new Quaternion(this.transform.rotation.x, this.transform.rotation.y, 0, this.transform.rotation.w);
					time = 0;
				}

				this.transform.rotation = new Quaternion(this.transform.rotation.x, this.transform.rotation.y, this.transform.rotation.z + 0.01f, this.transform.rotation.w);
			}

			// cursorPosとtargetの間にdots.Count個の点を配置
			for (int i = 1; i < dots.Count; i++)
			{
				dots[i - 1].transform.position = Vector3.Lerp(cursorPos, target, (float)i / dots.Count);
			}
		}
	}
}
