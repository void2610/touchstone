namespace NUI
{
    using UnityEngine;
    using UnityEngine.UI;
    using TMPro;
    using NManager;

    public class AltitudeUI : MonoBehaviour
    {
        private TextMeshProUGUI altitudeText;
        [SerializeField]
        private string prefix = "max: ";
        void Start()
        {
            altitudeText = gameObject.GetComponent<TextMeshProUGUI>();
            altitudeText.text = prefix + "0.0";
        }

        // Update is called once per frame
        void Update()
        {
            float a = GameManager.instance.maxAltitude;
            altitudeText.text = prefix + a.ToString("F2");
        }
    }
}
