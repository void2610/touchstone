using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JumpResetScript : MonoBehaviour
{
    public bool j = false;
    public GameObject player;
    PlayerMoveScript sc;
    Animator animator;
    public int jumpCount = 0;
    // Start is called before the first frame update
    void Start()
    {
        sc = player.GetComponent<PlayerMoveScript>();
        animator = player.GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {

    }

    void OnTriggerStay2D(Collider2D other) //地面に触れた時の処理
    {
        if(other.gameObject.tag != "Trigger"){
            sc.jumpable = true; //isGroundをtrueにする
            j = true;
            jumpCount = 0;
            //Debug.Log(other.gameObject.tag);
        }
    }

    void OnTriggerExsit2D(Collider2D other) //地面に触れた時の処理
    {
        animator.SetInteger("PlayerState", 2);
        j = false;
    }
}
