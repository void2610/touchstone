using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameControlScript : MonoBehaviour
{
    public int HP;
    public Sprite hrt;
    public Sprite halfHrt;
    public Sprite noneHrt;

    public int score = 0;

    float t = 0;
    public bool movable = true;

    GameObject[] hearts = new GameObject[5];
    void Start()
    {
        hearts[0] = GameObject.Find("Heart1");
        hearts[1] = GameObject.Find("Heart2");
        hearts[2] = GameObject.Find("Heart3");
        hearts[3] = GameObject.Find("Heart4");
        hearts[4] = GameObject.Find("Heart5");
        PlayerPrefs.SetInt("score", 0);
        if(PlayerPrefs.GetInt("highScore") == null){
            PlayerPrefs.SetInt("highScore", 0);
        }
        PlayerPrefs.Save();
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKey(KeyCode.P)){
            PlayerPrefs.SetInt("score", 0);
            PlayerPrefs.SetInt("highScore", 0);
            PlayerPrefs.Save();
        }
        if(HP <= 0){
            t += Time.deltaTime;
            movable = false;
            if(t >= 1){
                if(score > PlayerPrefs.GetInt("highScore")){
                    PlayerPrefs.SetInt("highScore", score);
                }
                PlayerPrefs.SetInt("score", score);
                PlayerPrefs.Save();
                SceneManager.LoadScene("GameOverScene");
            }
        }

        for(int i = 0; i < 5; i++){
                hearts[i].GetComponent<Image>().sprite = noneHrt;
            }
        int hrtNum = 0;
        int halfNum = 0;
        if(HP % 2 == 0){
            hrtNum = HP/2;
        }
        else{
            halfNum = 1;
            hrtNum = (HP-1)/2;
        }

        for(int i = 0; i < hrtNum; i++){
            hearts[i].GetComponent<Image>().sprite = hrt;
        }
        if(halfNum == 1){
            hearts[hrtNum].GetComponent<Image>().sprite = halfHrt;
        }
        if(hrtNum+1 < 5){
            for(int i = hrtNum+1; i <= 4; i++){
                hearts[i].GetComponent<Image>().sprite = noneHrt;
            }
        }
    }
}
