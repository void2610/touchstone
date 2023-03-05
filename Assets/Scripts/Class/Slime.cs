using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Slime : Enemy
{
    public override void SetStatus()
    {
        name = "Slime";
        hp = 1;
        atk = 1;
        killScore = 1;
    }

    void attack()
    {
    }
}
