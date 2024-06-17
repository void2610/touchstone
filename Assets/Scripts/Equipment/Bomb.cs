namespace NEquipment
{
	using System.Collections;
	using System.Collections.Generic;
	using UnityEngine;
	using UnityEngine.Tilemaps;

	public class Bomb : Equipment
	{
		private Vector3 bombAngle;
		private float throwPower = 5;
		private Tilemap tm;
		private int radius = 5;

		protected override void Awake()
		{
			name = "Bomb";
			actionKey = "Fire3";
			isCooling = false;
			coolTimeLength = 4.0f;
			isEnable = true;
			isActive = false;
			activeTimeLength = 3f;
		}

		protected IEnumerator Exprosion()
		{
			yield return new WaitForSeconds(4);

			//現在位置の周りのタイルを円形に破壊
			for (int i = (int)this.transform.position.x - radius; i <= (int)this.transform.position.x + radius; i++)
			{
				for (int j = (int)this.transform.position.y - radius; j <= (int)this.transform.position.y + radius; j++)
				{
					if (Mathf.Pow(i - this.transform.position.x, 2) + Mathf.Pow(j - this.transform.position.y, 2) <= Mathf.Pow(radius, 2))
					{

						if (i >= 0 && j >= 0 && i < 250 && j < 250)
						{
							tm.SetTile(new Vector3Int(i, j, 0), null);
						}
					}
				}
			}

			this.transform.position = new Vector3(0, 0, -10);
			this.GetComponent<Rigidbody2D>().gravityScale = 0;
		}

		protected override void OnActionStart()
		{

			throwPower = 5;
			this.GetComponent<Rigidbody2D>().gravityScale = 0;
			this.GetComponent<Rigidbody2D>().velocity = Vector2.zero;
			StartCoroutine(Exprosion());
		}

		protected override void Effect()
		{
			bombAngle = new Vector3(Mathf.Cos(getMouseAngle() * Mathf.Deg2Rad) * 1.4f, Mathf.Sin(getMouseAngle() * Mathf.Deg2Rad), 0);
			this.transform.position = player.transform.position + bombAngle * 1.8f;
			throwPower += 0.15f;
		}

		protected override void OnActionEnd()
		{
			this.GetComponent<Rigidbody2D>().gravityScale = 1;
			this.GetComponent<Rigidbody2D>().AddForce(bombAngle * throwPower, ForceMode2D.Impulse);
		}

		protected override void Start()
		{
			base.Start();
			this.transform.position = new Vector3(0, 0, -10);
			this.GetComponent<Rigidbody2D>().gravityScale = 0;
			tm = GameObject.Find("Tilemap").GetComponent<Tilemap>();
		}
	}
}
