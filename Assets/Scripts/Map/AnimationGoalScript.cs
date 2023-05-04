using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimationGoalScript : MonoBehaviour
{
	// Start is called before the first frame update
	void Start()
	{

	}

	// Update is called once per frame
	void Update()
	{
		//少しづつ回転させる
		this.transform.Rotate(new Vector3(0, 0, 0.03f));

		//少し上下に動かす
		this.transform.position = new Vector3(this.transform.position.x, this.transform.position.y + Mathf.Sin(Time.time) * 0.001f, this.transform.position.z);
	}
}
