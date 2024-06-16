namespace NEquipment
{
	using System.Collections;
	using System.Collections.Generic;
	using UnityEngine;
	using NCharacter;
	using UnityEngine.UI;

	public class Weapon : Equipment
	{
		public int attackPower;

		public int moveRadius;

		private float localScaleX;

		public Vector3 playerPosition;

		private Vector3 mousePosition;

		private Vector3 difference;

		private GameObject damageText;

		protected void MoveWeaponAngle()
		{
			difference = Camera.main.ScreenToWorldPoint(mousePosition) - playerPosition;
			angle = getMouseAngle();
			transform.eulerAngles = new Vector3(0f, 0f, angle - 45);
		}

		protected void MoveWeaponPosition()
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

		protected IEnumerator ChangeColortoRed(GameObject target)
		{
			target.GetComponent<SpriteRenderer>().color = Color.red;
			yield return new WaitForSeconds(0.3f);
			if (target != null)
			{
				target.GetComponent<SpriteRenderer>().color = Color.white;
			}
			yield return null;
		}

		protected IEnumerator HitStop()
		{
			Time.timeScale = 0f;
			yield return new WaitForSecondsRealtime(0.1f);
			Time.timeScale = 1;
		}

		protected void ProvideDamage(GameObject target)
		{
			target.GetComponent<Character>().hp -= attackPower;
			StartCoroutine(ChangeColortoRed(target));

			Debug.Log(target.name + "を攻撃した");
		}

		protected override void Start()
		{
			base.Start();
			damageText = Resources.Load<GameObject>("Prefabs/DamageText");
		}
	}
}
