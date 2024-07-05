namespace NEquipment
{
	using System.Collections;
	using System.Collections.Generic;
	using UnityEngine;
	using UnityEngine.UI;

	public class Grapple : Equipment
	{
		private SpringJoint2D joint;
		private LineRenderer lineRenderer;
		private float startDistance = 0;

		protected override void Awake()
		{
			base.Awake();
			equipmentName = "Grapple";
			coolTimeLength = 7.5f;
			activeTimeLength = 3f;
		}

		protected override void Effect()
		{
			base.Effect();
		}

		protected override void OnActionStart()
		{
			base.OnActionStart();
			lineRenderer.enabled = true;
			joint.enabled = true;
			joint.distance = Vector2.Distance(player.transform.position, this.transform.position);
			startDistance = joint.distance;

			this.transform.position = new Vector3(activeStartPosition.x, activeStartPosition.y, 0);
		}

		protected override void OnActionEnd()
		{
			base.OnActionEnd();
			joint.enabled = false;
			lineRenderer.enabled = false;
			lineRenderer.SetPosition(0, Vector2.zero);
			lineRenderer.SetPosition(1, Vector2.zero);
		}

		protected override void Start()
		{
			base.Start();
			joint = this.GetComponent<SpringJoint2D>();
			joint.connectedBody = player.GetComponent<Rigidbody2D>();
			joint.enabled = false;

			lineRenderer = this.GetComponent<LineRenderer>();
			lineRenderer.positionCount = 2;
		}
		protected override void FixedUpdate()
		{
			base.FixedUpdate();
			if (joint.enabled)
			{
				lineRenderer.SetPosition(0, player.transform.position);
				lineRenderer.SetPosition(1, this.transform.position);

				float sc = (Time.time - activeStartTime) / activeTimeLength;
				joint.distance = Mathf.Lerp(joint.distance, 0, sc * 0.1f * intensity);
			}
		}
	}
}
