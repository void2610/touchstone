using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DamageTextScript : MonoBehaviour
{
	private IEnumerator MoveText()
	{
		for (int i = 0; i < 50; i++)
		{
			float up = (50 - i) * 0.0005f;
			this.transform.position += new Vector3(0, up, 0);
			yield return new WaitForSeconds(0.001f);
		}
		yield return new WaitForSeconds(0.8f);
		Destroy(this.gameObject);
		yield break;
	}

	void Start()
	{
		StartCoroutine(MoveText());
	}

	// Update is called once per frame
	void Update()
	{

	}
}
