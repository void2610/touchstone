using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JumpResetScript : MonoBehaviour
{
	public bool j = false;

	public GameObject player;

	Animator animator;

	public int jumpCount = 0;

	void Start()
	{
		animator = player.GetComponent<Animator>();
	}

	void Update()
	{
	}

	void OnTriggerStay2D(Collider2D other)
	{
		if (other.gameObject.tag != "Trigger")
		{
			j = true;
			jumpCount = 0;
		}
	}

	void OnTriggerExsit2D(Collider2D other)
	{
		animator.SetInteger("PlayerState", 2);
		j = false;
	}
}
