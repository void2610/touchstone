namespace NEquipment
{
	using System.Collections;
	using System.Collections.Generic;
	using UnityEngine;
	using UnityEngine.UI;

	public class Grapple : Equipment
	{
		// private float hookLength = 10;
		private Vector2 playerPosition;
		private Vector2 direction;
		private Rigidbody2D rb;
		private SpringJoint2D joint;
		private LineRenderer lineRenderer;

		protected override void Awake()
		{
			base.Awake();
			equipmentName = "Grapple";
			coolTimeLength = 1.0f;
			activeTimeLength = 3f;
		}

		protected override void Effect()
		{
			base.Effect();
		}

		protected override void OnActionStart()
		{
			base.OnActionStart();
			playerPosition = player.GetComponent<Rigidbody2D>().position;
			direction = activeStartPosition - playerPosition;

			lineRenderer.enabled = true;

			joint.enabled = true;
			joint.connectedAnchor = activeStartPosition;
		}

		protected override void OnActionEnd()
		{
			base.OnActionEnd();
			joint.enabled = false;
			lineRenderer.enabled = false;
			rb.velocity = Vector2.zero;
			lineRenderer.SetPosition(0, Vector2.zero);
			lineRenderer.SetPosition(1, Vector2.zero);
		}

		protected override void Start()
		{
			base.Start();
			rb = this.GetComponent<Rigidbody2D>();
			joint = player.GetComponent<SpringJoint2D>();
			joint.enabled = false;

			lineRenderer = this.GetComponent<LineRenderer>();
			lineRenderer.positionCount = 2;

			player.GetComponent<SpringJoint2D>().enabled = false;
		}
		protected override void FixedUpdate()
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
