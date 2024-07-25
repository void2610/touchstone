namespace NUI
{
    using UnityEngine;
    using UnityEngine.UI;
    using TMPro;
    using NManager;

    public class AltitudeMarker : MonoBehaviour
    {
        private Vector3 defaultPosition => this.transform.position;

        void Update()
        {
            float target = GameManager.instance.maxAltitude - GameManager.instance.altitudeOffset;
            this.transform.position = Vector3.Lerp(this.transform.position, new Vector3(defaultPosition.x, target, defaultPosition.z), 3 * Time.deltaTime);
        }
    }
}
