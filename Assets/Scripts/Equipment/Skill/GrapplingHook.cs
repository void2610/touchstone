namespace NEquipment
{
	using System.Collections;
	using System.Collections.Generic;
	using UnityEngine;

	public class GrapplingHook : Skill
	{
		private float hookLength = 999;
		private float hookPullSpeed = 3;
		private Vector2 playerPosition;
		private Vector2 direction;

		private float distance = 0;

		private Rigidbody2D rb;

		private SpringJoint2D joint;
		private LineRenderer lineRenderer;

		public void Awake()
		{
			name = "GrapplingHook";
			actionKey = "Fire2";
			isCooling = false;
			coolTimeLength = 1.0f;
			isEnable = true;
			isActive = false;
			activeTimeLength = 15;
		}

		public override void Effect()
		{
		}

		public override void OnActionStart()
		{
			playerPosition = player.GetComponent<Rigidbody2D>().position;

			direction = activeStartPosition - playerPosition;

			player.GetComponent<Rigidbody2D>().AddForce(direction * 5);

			distance = direction.magnitude;
			if (distance < hookLength)
			{
				distance = hookLength;
			}

			lineRenderer.enabled = true;

			joint.enabled = true;
			joint.connectedAnchor = activeStartPosition;
			joint.distance = 0.5f;
			joint.frequency = hookPullSpeed;
		}

		public override void OnActionEnd()
		{
			joint.enabled = false;
			lineRenderer.enabled = false;
			rb.velocity = Vector2.zero;
		}

		public override void Start()
		{
			base.Start();
			rb = this.GetComponent<Rigidbody2D>();
			joint = player.GetComponent<SpringJoint2D>();
			joint.enabled = false;
			lineRenderer = this.GetComponent<LineRenderer>();
			lineRenderer.positionCount = 2;
		}
		public override void Update()
		{
			base.Update();
			if (joint.enabled)
			{

				Vector3 pos = new Vector3(joint.connectedAnchor.x, joint.connectedAnchor.y, -1);
				lineRenderer.SetPosition(0, player.transform.position);
				lineRenderer.SetPosition(1, pos);
			}
		}
	}
}