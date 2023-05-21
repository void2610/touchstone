namespace NUI
{


	using System.Collections;
	using System.Collections.Generic;
	using UnityEngine;
	using NManager;

	public class MoveMouseCursorScript : MonoBehaviour
	{
		public Texture2D cursorTexture;
		public GameObject cursor;
		Vector3 position;
		private Vector3 screenToWorldPointPosition;
		float time = 0;
		void Start()
		{
			Cursor.visible = false;
		}

		// Update is called once per frame
		void Update()
		{
			time += Time.deltaTime;
			// Vector3でマウス位置座標を取得する
			position = Input.mousePosition;
			// Z軸修正
			position.z = 10f;
			// マウス位置座標をスクリーン座標からワールド座標に変換する
			screenToWorldPointPosition = Camera.main.ScreenToWorldPoint(position);
			// ワールド座標に変換されたマウス座標を代入
			if (GameManager.instance.state != GameManager.GameState.GameOver && GameManager.instance.state != GameManager.GameState.Other)
			{
				cursor.transform.position = screenToWorldPointPosition;

				if (time >= 2)
				{
					cursor.transform.rotation = new Quaternion(cursor.transform.rotation.x, cursor.transform.rotation.y, 0, cursor.transform.rotation.w);
					time = 0;
				}

				cursor.transform.rotation = new Quaternion(cursor.transform.rotation.x, cursor.transform.rotation.y, cursor.transform.rotation.z + 0.01f, cursor.transform.rotation.w);
				Cursor.SetCursor(cursorTexture, Vector2.zero, CursorMode.Auto);
			}
		}
	}
}