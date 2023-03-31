namespace NEquipment
{
	using System.Collections;
	using System.Collections.Generic;
	using UnityEngine;

	public class Wepon : Equipment
	{
		public int attackPower;

		public int moveRadius;

		private float localScaleX;

		private GameObject player;

		public Vector3 playerPosition;

		private Vector3 mousePosition;

		private Vector3 difference;

		public float angle;
		public void MoveWeapon()
		{
			player = GameObject.Find("Player");

			playerPosition = player.transform.position;
			playerPosition.x -= 0.4f;
			playerPosition.y -= 0.4f;

			mousePosition = Input.mousePosition;
			mousePosition.z = 10f;

			// マウス位置座標をスクリーン座標からワールド座標に変換する
			difference = Camera.main.ScreenToWorldPoint(mousePosition) - playerPosition;
			angle = Mathf.Atan2(difference.y, difference.x) * Mathf.Rad2Deg;

			var radian = angle * (Mathf.PI / 180);
			this.transform.position = new Vector3(Mathf.Cos(radian) * moveRadius + 10, Mathf.Sin(radian) * moveRadius - 25, 0).normalized + playerPosition;
			transform.eulerAngles = new Vector3(0f, 0f, angle - 45);
		}

		public virtual void Start()
		{
			//localScaleX = gameObject.transform.localScale.x;
		}
	}
}
