namespace NUI
{
    using UnityEngine;
    using UnityEngine.EventSystems;
    using UnityEngine.UI;
    using DG.Tweening;

    public class TweenButton : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
    {
        [Header("Button Settings")]
        [SerializeField]
        private bool tweenByPointer = true;
        [SerializeField]
        private bool tweenByClick = true;

        [Header("Tween Settings")]
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
            if (tweenByPointer)
                this.transform.DOScale(defaultScale * scale, duration).SetEase(Ease.OutElastic).SetUpdate(true);
        }

        public void OnPointerExit(PointerEventData eventData)
        {
            if (tweenByPointer)
                this.transform.DOScale(defaultScale, duration).SetEase(Ease.OutElastic).SetUpdate(true);
        }

        private void Awake()
        {
            defaultScale = this.transform.localScale.x;
            if (tweenByClick)
            {
                if (this.GetComponent<Button>() != null)
                {
                    this.GetComponent<Button>().onClick.AddListener(OnClick);
                }
            }
        }

        private void Start()
        {
            defaultScale = this.transform.localScale.x;
        }
    }
}
