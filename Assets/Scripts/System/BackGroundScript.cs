using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BackGroundScript : MonoBehaviour
{
	[SerializeField]
	private Sprite backGround;

	private float s = 3f;
	private Vector3 scale;

	private GameObject player;

	private Vector3 position;

	private float backGroundWidth;

	private int inverse = -1;

	void CreateBackGround(Vector3 position)
	{
		//backGroundをゲームオブジェクトとして2D空間に配置するコード
		GameObject backGroundObject = new GameObject("BackGround");
		backGroundObject.transform.position = position;
		inverse *= -1;
		scale = new Vector3(s * inverse, s, 1);
		backGroundObject.transform.localScale = scale;
		backGroundObject.AddComponent<SpriteRenderer>();
		backGroundObject.GetComponent<SpriteRenderer>().sprite = backGround;
		backGroundObject.GetComponent<SpriteRenderer>().sortingOrder = -1;
	}

	void Start()
	{
		scale = new Vector3(s, s, 1);

		backGroundWidth = backGround.bounds.size.x * scale.x;
		player = GameObject.Find("Player");
		position = new Vector3(0, 7, 0);
		CreateBackGround(position);
	}

	// Update is called once per frame
	void Update()
	{
		//プレイヤーの位置に応じて背景を新しく重ならずに生成するコード
		if (player.transform.position.x + 150 > position.x + backGroundWidth)
		{
			position.x += backGroundWidth;
			CreateBackGround(position);
		}
	}
}
