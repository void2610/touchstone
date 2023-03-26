using System.Collections;
using System.Collections.Generic;
using NCharacter;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameControlScript : MonoBehaviour
{
    public int HP;

    public int score = 0;

    float t = 0;

    public bool movable = true;

    Character player;

    void Start()
    {
        player = GameObject.Find("Player").GetComponent<Character>();
        PlayerPrefs.SetInt("score", 0);
        if (PlayerPrefs.GetInt("highScore") == null)
        {
            PlayerPrefs.SetInt("highScore", 0);
        }
        PlayerPrefs.Save();
    }

    // Update is called once per frame
    void Update()
    {
        HP = player.hp;

        //簡易スコアリセット
        if (Input.GetKey(KeyCode.P))
        {
            PlayerPrefs.SetInt("score", 0);
            PlayerPrefs.SetInt("highScore", 0);
            PlayerPrefs.Save();
        }
        if (HP <= 0)
        {
            t += Time.deltaTime;
            movable = false;
            if (t >= 1)
            {
                if (score > PlayerPrefs.GetInt("highScore"))
                {
                    PlayerPrefs.SetInt("highScore", score);
                }
                PlayerPrefs.SetInt("score", score);
                PlayerPrefs.Save();
                SceneManager.LoadScene("GameOverScene");
            }
        }
    }
}
