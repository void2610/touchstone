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

		private Vector3 playerPosition;

		private Vector3 mousePosition;

		private Vector3 difference;

		public float angle;

		public virtual IEnumerator Action()
		{
			base.Action();
			yield break;
		}

		public void MoveWeapon()
		{
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
			base.Start();
			name = "Wepon";
			actionKey = "Mouse0";
			coolTimeLength = 1f;
			isEnable = true;
			isActive = false;
			activeTimeLength = 1f;
			attackPower = 1;
			moveRadius = 60;

			localScaleX = gameObject.transform.localScale.x;
			player = GameObject.Find("Player");
		}

		public virtual void Update()
		{
			base.Update();
			MoveWeapon();
		}

		public virtual void FixedUpdate()
		{
			base.FixedUpdate();
		}
	}
}
