namespace NManager
{
    using UnityEngine;
    using UnityEngine.UI;
    using UnityEngine.SceneManagement;
    using System.Collections.Generic;
    using TMPro;
    using NUI;
    using DG.Tweening;

    public class UIManager : MonoBehaviour
    {
        [SerializeField]
        private List<CanvasGroup> canvasGroups;
        [SerializeField]
        private TextMeshProUGUI resultText;
        [SerializeField]
        private TextMeshProUGUI gaindCoinText;

        public void GoToTitle()
        {
            SceneManager.LoadScene("TitleScene");
        }

        public void SetResultText(float target)
        {
            DOTween.To(() => 0, x => resultText.text = "max: " + x.ToString("F2") + "m", target, 0.2f + target / 1000.0f);
        }

        public void SetGaindCoinText(string text)
        {
            gaindCoinText.text = text;
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
                case GameManager.GameState.Other:
                    break;
            }
        }

        private void Start()
        {
            ChangeUIState(GameManager.GameState.Playing);
        }
    }
}
