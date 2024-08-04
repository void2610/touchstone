namespace NUI
{
    using UnityEngine;
    using UnityEngine.EventSystems;
    using UnityEngine.UI;
    using DG.Tweening;

    public class TweenButton : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
    {
        [SerializeField]
        private float scale = 1.1f;
        [SerializeField]
        private float duration = 0.5f;
        private float defaultScale = 1.0f;
        private void OnClick()
        {
            this.transform.DOScale(defaultScale * scale, duration).SetEase(Ease.OutElastic);
        }

        public void OnPointerEnter(PointerEventData eventData)
        {
            this.transform.DOScale(defaultScale * scale, duration).SetEase(Ease.OutElastic);
        }

        public void OnPointerExit(PointerEventData eventData)
        {
            this.transform.DOScale(defaultScale, duration).SetEase(Ease.OutElastic);
        }

        private void Awake()
        {
            defaultScale = this.transform.localScale.x;
            // Add Event Listener for button click
            this.GetComponent<Button>().onClick.AddListener(OnClick);
        }
    }
}
