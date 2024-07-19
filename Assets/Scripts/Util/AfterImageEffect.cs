using System.Collections;
using UnityEngine;

public class AfterImageEffect : MonoBehaviour
{
    [SerializeField]
    private float afterImageLifetime = 0.5f;
    [SerializeField]
    private float afterImageInterval = 0.1f;
    [SerializeField]
    private Color color = new Color(1f, 1f, 1f, 0.5f);
    public bool isCreateAfterImage = false;
    private SpriteRenderer spriteRenderer;

    void Start()
    {
        spriteRenderer = this.GetComponent<SpriteRenderer>();
        StartCoroutine(CreateAfterImages());
    }

    IEnumerator CreateAfterImages()
    {
        while (true)
        {
            if (isCreateAfterImage)
            {
                CreateAfterImage();
            }
            yield return new WaitForSeconds(afterImageInterval);
        }
    }

    void CreateAfterImage()
    {
        GameObject afterImage = new GameObject("AfterImage");
        SpriteRenderer sr = afterImage.AddComponent<SpriteRenderer>();
        sr.sprite = spriteRenderer.sprite;
        sr.color = this.color;

        afterImage.transform.position = this.transform.position;
        afterImage.transform.rotation = this.transform.rotation;
        afterImage.transform.localScale = this.transform.localScale;

        Destroy(afterImage, afterImageLifetime); // 残像の寿命が来たら削除
        StartCoroutine(FadeOutAfterImage(sr, afterImageLifetime));
    }

    IEnumerator FadeOutAfterImage(SpriteRenderer sr, float lifetime)
    {
        float startAlpha = sr.color.a;
        float rate = 1.0f / lifetime;
        float progress = 0.0f;

        while (progress < 1.0f)
        {
            if (sr == null) yield break;
            Color tmpColor = sr.color;
            tmpColor.a = Mathf.Lerp(startAlpha, 0, progress);
            sr.color = tmpColor;

            progress += rate * Time.deltaTime;

            yield return null;
        }
    }
}
