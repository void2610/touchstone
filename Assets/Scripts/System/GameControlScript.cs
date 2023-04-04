namespace NControl
{
    using System.Collections;
    using System.Collections.Generic;
    using System.Threading.Tasks;
    using NCharacter;
    using UnityEngine;
    using UnityEngine.SceneManagement;
    using UnityEngine.UI;

    public class GameControlScript : MonoBehaviour
    {
        public int score = 0;

        public bool movable = true;

        Character player;

        public void ResetScore()
        {
            PlayerPrefs.SetInt("score", 0);
            PlayerPrefs.SetInt("highScore", 0);
            PlayerPrefs.Save();
        }

        public void SaveScore()
        {
            if (score > PlayerPrefs.GetInt("highScore"))
            {
                PlayerPrefs.SetInt("highScore", score);
            }
            PlayerPrefs.SetInt("score", score);
            PlayerPrefs.Save();
        }

        async void PlayerDeath()
        {
            movable = false;
            SaveScore();
            await Task.Delay(1000);
        }

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

        void Update()
        {
            //簡易スコアリセット
            if (Input.GetKey(KeyCode.P))
            {
                ResetScore();
            }

            //死亡
            if (player.hp <= 0)
            {
                PlayerDeath();
                SceneManager.LoadScene("GameOverScene");
            }
        }
    }
}
