using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ShowScoreScript : MonoBehaviour
{

    public GameObject gc;
    GameControlScript gcScript;

    void Start()
    {
        if(gc != null){
            gcScript = gc.GetComponent<GameControlScript>();
        }
    }

    // Update is called once per frame
    void Update()
    {
        Text score_text = gameObject.GetComponent<Text> ();
        // テキストの表示を入れ替える
        if(this.name == "ScoreText"){
            if(gc == null){
                score_text.text = "Score:" + PlayerPrefs.GetInt("score").ToString();
            }
            else
            {
                score_text.text = "Score:" + gcScript.score;
            }
        }
        else if(this.name == "HighScoreText"){
            score_text.text = "Highcore:" + PlayerPrefs.GetInt("highScore").ToString();
        }
    }
}
