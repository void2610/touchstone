using UnityEngine;
using UnityEngine.EventSystems;
using DG.Tweening;

public class ShakeOnStayPointer : MonoBehaviour
{
    [SerializeField]
    private float strength;
    [SerializeField]
    private int vibrato;
    [SerializeField]
    private float randomness;
    public void StartShake()
    {
        // ずっと揺れる
        this.transform.DOShakePosition(1.0f, strength, vibrato, randomness, false, false).SetLoops(-1);
    }

    public void StopShake()
    {
        transform.DOKill();
    }

    private void Awake()
    {
        EventTrigger t = this.GetComponent<EventTrigger>();
        if (t == null)
        {
            t = this.gameObject.AddComponent<EventTrigger>();
        }
        // Add PointerEnter event
        EventTrigger.Entry entry = new EventTrigger.Entry();
        entry.eventID = EventTriggerType.PointerEnter;
        entry.callback.AddListener((data) => { StartShake(); });
        t.triggers.Add(entry);
        // Add PointerExit event
        entry = new EventTrigger.Entry();
        entry.eventID = EventTriggerType.PointerExit;
        entry.callback.AddListener((data) => { StopShake(); });
        t.triggers.Add(entry);
    }
}
