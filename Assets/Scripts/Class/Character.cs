using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Character : MonoBehaviour
{
    public string name;

    public int hp;

    public int atk;

    public int killScore;

    //相手のHPを減らす機能
    public void CutHP(Character target)
    {
        target.hp -= atk;
        Debug.Log(target.name + "のHPが" + atk + "削れた");
    }

    public Character SearchCharacter(GameObject target)
    {
        Character result = null;

        if (target.GetComponent<Character>() != null)
        {
            result = target.GetComponent<Character>();
        }

        return result;
    }

    //ステータス色々設定
    public virtual void SetStatus()
    {
        name = "NoName";
    }

    public void Start()
    {
        SetStatus();
    }

    public void Update()
    {
    }
}
