using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMoveScript : MonoBehaviour
{
	[SerializeField]
	float offset;
	public GameObject player;
	Vector2 plapos;
	void Start()
	{

	}

	// Update is called once per frame
	void Update()
	{
		plapos = player.transform.position;
		this.transform.position = new Vector3(plapos.x, plapos.y + offset, -10);
	}
}
