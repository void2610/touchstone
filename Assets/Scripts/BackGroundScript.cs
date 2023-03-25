using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BackGroundScript : MonoBehaviour
{
    [SerializeField]
    private Sprite backGround;

    [SerializeField]
    private Vector3 scale = new Vector3(11, 11, 11);

    private GameObject player;

    private Vector3 position;

    private float backGroundWidth;

    void CreateBackGround(Vector3 position)
    {
        //backGroundをゲームオブジェクトとして2D空間に配置するコード
        GameObject backGroundObject = new GameObject("BackGround");
        backGroundObject.transform.position = position;
        backGroundObject.transform.localScale = scale;
        backGroundObject.AddComponent<SpriteRenderer>();
        backGroundObject.GetComponent<SpriteRenderer>().sprite = backGround;
        backGroundObject.GetComponent<SpriteRenderer>().sortingOrder = -1;
    }

    void Start()
    {
        backGroundWidth = backGround.bounds.size.x * scale.x;
        player = GameObject.Find("Player");
        position = new Vector3(0, 0, 0);
        CreateBackGround (position);
    }

    // Update is called once per frame
    void Update()
    {
        //プレイヤーの位置に応じて背景を新しく重ならずに生成するコード
        if (player.transform.position.x + 50 > position.x + backGroundWidth)
        {
            position.x += backGroundWidth;
            CreateBackGround (position);
        }
    }
}
