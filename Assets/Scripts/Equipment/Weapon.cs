namespace NEquipment
{
	using System.Collections;
	using System.Collections.Generic;
	using UnityEngine;

	public class Weapon : Equipment
	{
		public int attackPower;

		public int moveRadius;

		private float localScaleX;

		public Vector3 playerPosition;

		private Vector3 mousePosition;

		private Vector3 difference;

		public void MoveWeaponAngle()
		{
			difference = Camera.main.ScreenToWorldPoint(mousePosition) - playerPosition;
			angle = getMouseAngle();
			transform.eulerAngles = new Vector3(0f, 0f, angle - 45);
		}

		public void MoveWeaponPosition()
		{
			playerPosition = player.transform.position;
			playerPosition.x -= 0.4f;
			playerPosition.y -= 0.4f;

			mousePosition = Input.mousePosition;
			mousePosition.z = 10f;

			// マウス位置座標をスクリーン座標からワールド座標に変換する
			difference = Camera.main.ScreenToWorldPoint(mousePosition) - playerPosition;

			var radian = angle * (Mathf.PI / 180);

			this.transform.position = new Vector3(Mathf.Cos(radian) * moveRadius + 10, Mathf.Sin(radian) * moveRadius - 25, 0).normalized + playerPosition;
		}

		public override void Start()
		{
			base.Start();
		}
	}
}
