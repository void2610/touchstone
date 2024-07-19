using UnityEngine;

public class DisableComponent : MonoBehaviour
{
    [SerializeField]
    private SpriteRenderer targetComponent;
    [SerializeField]
    private float delay = 0.1f;

    private void Disable()
    {
        targetComponent.enabled = false;
    }

    private void Start()
    {
        Invoke("Disable", delay);
    }
}
