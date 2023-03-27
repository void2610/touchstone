namespace NUI
{
    using System.Collections;
    using System.Collections.Generic;
    using NControl;
    using UnityEngine;
    using UnityEngine.UI;

    public class ShowScoreScript : MonoBehaviour
    {
        void Start()
        {
            Debug.Log("score:" + PlayerPrefs.GetInt("score").ToString());
            Debug
                .Log("highScore:" + PlayerPrefs.GetInt("highScore").ToString());
        }

        void Update()
        {
            Text score_text = gameObject.GetComponent<Text>();

            // テキストの表示を入れ替える
            if (this.name == "ScoreText")
            {
                score_text.text =
                    "Score:" + PlayerPrefs.GetInt("score").ToString();
            }
            else if (this.name == "HighScoreText")
            {
                score_text.text =
                    "Highcore:" + PlayerPrefs.GetInt("highScore").ToString();
            }
        }
    }
}
