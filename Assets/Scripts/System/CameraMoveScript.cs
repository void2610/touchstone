using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMoveScript : MonoBehaviour
{
    public GameObject player;
    Vector2 plapos;
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        plapos = player.transform.position;
        if(plapos.x > 5 && plapos.x < 1300){
            this.transform.position = new Vector3(plapos.x,this.transform.position.y,-10);
        }
        if(plapos.y < 6){
            this.transform.position = new Vector3(this.transform.position.x,plapos.y+1,-10);
        }
    }
}
