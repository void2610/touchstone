using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyHPScript : MonoBehaviour
{
    public int HP = 1;
    public int killScore = 3;
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if(HP == 0){
            Destroy(this.gameObject);
        }
    }
}
