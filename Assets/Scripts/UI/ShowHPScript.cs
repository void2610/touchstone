namespace NUI
{
    using System.Collections;
    using System.Collections.Generic;
    using NCharacter;
    using UnityEngine;
    using UnityEngine.UI;

    public class ShowHPScript : MonoBehaviour
    {
        public float HP;

        public Sprite hrt;

        public Sprite halfHrt;

        public Sprite noneHrt;

        GameObject[] hearts = new GameObject[5];

        Character player;

        void Start()
        {
            player = GameObject.Find("Player").GetComponent<Character>();

            hearts[0] = GameObject.Find("Heart1");
            hearts[1] = GameObject.Find("Heart2");
            hearts[2] = GameObject.Find("Heart3");
            hearts[3] = GameObject.Find("Heart4");
            hearts[4] = GameObject.Find("Heart5");
        }

        void Update()
        {
            HP = player.hp / 2f;
            int n = (int) Mathf.Floor(HP);

            //(⋈◍＞◡＜◍)。✧♡描画処理
            for (int i = 0; i < 5; i++)
            {
                hearts[i].GetComponent<Image>().sprite = noneHrt;
            }
            bool isHalf = false;
            if (player.hp % 2 != 0)
            {
                isHalf = true;
            }

            for (int i = 0; i < n; i++)
            {
                hearts[i].GetComponent<Image>().sprite = hrt;
            }
            if (isHalf)
            {
                hearts[n].GetComponent<Image>().sprite = halfHrt;
            }
            if (n < 4)
            {
                for (int i = n + 1; i <= 4; i++)
                {
                    hearts[i].GetComponent<Image>().sprite = noneHrt;
                }
            }
        }
    }
}
