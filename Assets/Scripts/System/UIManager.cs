namespace NManager
{
    using System;
    using System.Collections.Generic;
    using System.Collections;
    using UnityEngine;
    using UnityEngine.UI;
    using UnityEngine.SceneManagement;
    using TMPro;
    using NUI;
    using DG.Tweening;

    public class UIManager : MonoBehaviour
    {
        [SerializeField]
        private Image fadeImage;
        [SerializeField]
        private List<CanvasGroup> canvasGroups;
        [SerializeField]
        private TextMeshProUGUI resultText;
        [SerializeField]
        private TextMeshProUGUI clearTimeText;
        [SerializeField]
        private TextMeshProUGUI gaindCoinText;
        [SerializeField]
        private TextMeshProUGUI gaindCoinText2;
        [SerializeField]
        private TextMeshProUGUI inGameTimeText;

        public void FadeIn(string loadSceneName = "")
        {
            fadeImage.DOFade(0, 0).SetUpdate(true);
            fadeImage.DOFade(1, 1.0f).SetUpdate(true).OnComplete(() =>
            {
                if (loadSceneName != "") SceneManager.LoadScene(loadSceneName);
            });
        }

        public void FadeOut()
        {
            fadeImage.DOFade(1, 0).SetUpdate(true);
            fadeImage.DOFade(0, 1.0f).SetUpdate(true);
        }

        public void CrossFade(float duration, Action a1, Action a2)
        {
            fadeImage.DOFade(0, 0).SetUpdate(true);
            fadeImage.DOFade(1, duration).SetUpdate(true).OnComplete(() =>
            {
                a1();
                fadeImage.DOFade(0, duration).SetUpdate(true).OnComplete(() =>
                {
                    a2();
                });
            });
        }

        public void GoToTitle()
        {
            FadeIn("TitleScene");
        }

        public void SetResultText(float target)
        {
            DOTween.To(() => 0, x => resultText.text = "max: " + x.ToString("F2") + "m", target, 0.2f + target / 1000.0f);
        }

        public void SetClearTimeText(float t)
        {
            TimeSpan time = TimeSpan.FromSeconds(t);
            clearTimeText.text = "Clear Time: " + time.ToString(@"mm\:ss\:ff");
        }

        public void SetGaindCoinText(string text)
        {
            gaindCoinText.text = text;
            gaindCoinText2.text = text;
        }

        private void ChangeCanvasGroupEnabled(CanvasGroup c, bool enabled)
        {
            c.alpha = enabled ? 1 : 0;
            c.blocksRaycasts = enabled;
            c.interactable = enabled;
        }

        public void ChangeUIState(GameManager.GameState state)
        {
            foreach (var canvasGroup in canvasGroups)
            {
                ChangeCanvasGroupEnabled(canvasGroup, false);
            }

            switch (state)
            {
                case GameManager.GameState.Playing:
                    ChangeCanvasGroupEnabled(canvasGroups[0], true);
                    break;
                case GameManager.GameState.Paused:
                    ChangeCanvasGroupEnabled(canvasGroups[2], true);
                    break;
                case GameManager.GameState.GameOver:
                    ChangeCanvasGroupEnabled(canvasGroups[1], true);
                    break;
                case GameManager.GameState.Clear:
                    ChangeCanvasGroupEnabled(canvasGroups[3], true);
                    break;
                case GameManager.GameState.Other:
                    break;
            }
        }

        private void Start()
        {
            ChangeUIState(GameManager.GameState.Playing);
            FadeOut();
        }

        private void Update()
        {
            TimeSpan time = TimeSpan.FromSeconds(Time.timeSinceLevelLoad);
            inGameTimeText.text = "time: " + time.ToString(@"mm\:ss\:ff");
        }
    }
}
