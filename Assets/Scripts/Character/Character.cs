namespace NCharacter
{
    using System.Collections;
    using System.Collections.Generic;
    using UnityEngine;

    public class Character : MonoBehaviour
    {
        public string name;

        public int hp;

        public int atk;

        public int killScore;

        public int direction = 1; //-1 = 左  1 = 右

        public Vector2 firstLScale;

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

        public virtual void Start()
        {
            name = "NoName";
            hp = 1;
            atk = 1;
            killScore = 1;
            firstLScale = this.gameObject.transform.localScale;
        }

        public virtual void Update()
        {
            if (direction == 1)
            {
                this.gameObject.transform.localScale =
                    new Vector2(firstLScale.x, firstLScale.y);
            }
            else
            {
                this.gameObject.transform.localScale =
                    new Vector2(-firstLScale.x, firstLScale.y);
            }
        }
    }
}
