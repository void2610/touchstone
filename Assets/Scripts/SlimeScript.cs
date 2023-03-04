using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SlimeScript : MonoBehaviour
{
    // Start is called before the first frame update
    float time = 0;
    bool moving = false;
    float coolTime = 0;
    float sTime = 0;
    int cnt = 1;
    int subcnt = 1;
    public GameObject gCon;
    public float speed;
    void Start()
    {
        gCon = GameObject.Find("GameController");
    }

    // Update is called once per frame
    void Update()
    {
        time += Time.deltaTime;
            if(moving){
                if(cnt== 1){
                    this.gameObject.transform.position -= transform.right*speed;
                }
                else{
                    this.gameObject.transform.position += transform.right*speed;
                }
                if((time - sTime) > 1){
                    moving = false;
                    sTime = 0;
                    subcnt *= -1;
                    if(subcnt == -1){
                        cnt *= -1;
                    }
                }
            }
            else{
                coolTime += Time.deltaTime;
                if(coolTime > 1){
                    moving = true;
                    coolTime = 0;
                    sTime = time;
                }
            }
        }
}
