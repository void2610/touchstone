namespace NUI
{
    using UnityEngine;
    using UnityEngine.UI;
    using NManager;

    public class AltitudeUI : MonoBehaviour
    {
        private Text altitudeText;
        void Start()
        {
            altitudeText = gameObject.GetComponent<Text>();
            altitudeText.text = "Altitude: 0";
        }

        // Update is called once per frame
        void Update()
        {
            float a = GameManager.instance.maxAltitude;
            altitudeText.text = "Altitude: " + a.ToString("F2");
        }
    }
}
