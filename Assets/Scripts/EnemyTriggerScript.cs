using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyTriggerScript : MonoBehaviour
{
    public GameObject gCon;
    void Start()
    {
        gCon = GameObject.Find("GameController");
    }

    // Update is called once per frame
    void Update()
    {
    }
    void OnTriggerEnter2D(Collider2D other) //敵に触れた時の処理
    {
        if(other.gameObject.tag == "PlayerTrigger"){
            if(gCon.GetComponent<GameControlScript>().HP >= 1){
                gCon.GetComponent<GameControlScript>().HP--;
            }
        }
    }
}
