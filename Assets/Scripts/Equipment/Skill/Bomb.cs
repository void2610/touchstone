namespace NEquipment
{
	using System.Collections;
	using System.Collections.Generic;
	using UnityEngine;

	public class Bomb : Skill
	{
		private Vector3 bombAngle;
		private float throwPower = 5;

		public void Awake()
		{
			name = "Bomb";
			actionKey = "Fire3";
			isCooling = false;
			coolTimeLength = 1.0f;
			isEnable = true;
			isActive = false;
			activeTimeLength = 3f;
		}

		private IEnumerator Exprosion()
		{
			yield return new WaitForSeconds(4);
			this.transform.position = new Vector3(0, 0, -10);
			this.GetComponent<Rigidbody2D>().gravityScale = 0;
		}

		public override void OnActionStart()
		{

			throwPower = 5;
			Debug.Log("Bomb");
			this.GetComponent<Rigidbody2D>().gravityScale = 0;
			this.GetComponent<Rigidbody2D>().velocity = Vector2.zero;
			StartCoroutine(Exprosion());
		}

		public override void Effect()
		{
			bombAngle = new Vector3(Mathf.Cos(getMouseAngle() * Mathf.Deg2Rad) * 1.4f, Mathf.Sin(getMouseAngle() * Mathf.Deg2Rad), 0);
			this.transform.position = player.transform.position + bombAngle * 1.8f;
			Debug.Log(bombAngle);
			throwPower += 0.15f;
		}

		public override void OnActionEnd()
		{
			Debug.Log(throwPower);
			this.GetComponent<Rigidbody2D>().gravityScale = 1;
			this.GetComponent<Rigidbody2D>().AddForce(bombAngle * throwPower, ForceMode2D.Impulse);
		}

		public override void Start()
		{
			base.Start();
			this.transform.position = new Vector3(0, 0, -10);
			this.GetComponent<Rigidbody2D>().gravityScale = 0;
		}
	}
}
