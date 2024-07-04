namespace NUI
{
    using UnityEngine;
    using UnityEngine.UI;
    using TMPro;
    using NManager;

    public class AltitudeMarker : MonoBehaviour
    {
        private Vector3 defaultPosition;
        void Start()
        {
            defaultPosition = transform.position;
        }

        // Update is called once per frame
        void Update()
        {
            float a = GameManager.instance.maxAltitude;
            this.transform.position = Vector3.Lerp(this.transform.position, new Vector3(defaultPosition.x, a, defaultPosition.z), 3 * Time.deltaTime);
        }
    }
}
