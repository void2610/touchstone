namespace NEquipment
{
	using System.Collections;
	using System.Collections.Generic;
	using UnityEngine;

	public class Grapple : Skill
	{
		private float hookLength = 10;
		private Vector2 playerPosition;
		private Vector2 direction;
		private float frequency = 0;
		private Rigidbody2D rb;
		private SpringJoint2D joint;
		private LineRenderer lineRenderer;

		public void Awake()
		{
			name = "Grapple";
			actionKey = "Fire2";
			isCooling = false;
			coolTimeLength = 1.0f;
			isEnable = true;
			isActive = false;
			activeTimeLength = 3f;
		}

		public override void Effect()
		{
		}

		public override void OnActionStart()
		{
			playerPosition = player.GetComponent<Rigidbody2D>().position;
			direction = activeStartPosition - playerPosition;

			lineRenderer.enabled = true;

			joint.enabled = true;
			joint.connectedAnchor = activeStartPosition;
		}

		public override void OnActionEnd()
		{
			joint.enabled = false;
			lineRenderer.enabled = false;
			rb.velocity = Vector2.zero;
			lineRenderer.SetPosition(0, Vector2.zero);
			lineRenderer.SetPosition(1, Vector2.zero);
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
		public override void FixedUpdate()
		{
			base.FixedUpdate();
			if (joint.enabled)
			{
				Vector3 pos = new Vector3(joint.connectedAnchor.x, joint.connectedAnchor.y, -1);
				lineRenderer.SetPosition(0, player.transform.position);
				lineRenderer.SetPosition(1, pos);
			}
		}
	}
}