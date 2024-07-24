namespace NUI
{
    using UnityEngine;
    using UnityEngine.UI;
    using TMPro;
    using NManager;

    public class AltitudeUI : MonoBehaviour
    {
        private TextMeshProUGUI altitudeText => this.GetComponent<TextMeshProUGUI>();
        [SerializeField]
        private string prefix = "max: ";
        void Start()
        {
            altitudeText.text = prefix + "0.0";
        }

        void Update()
        {
            altitudeText.text = prefix + GameManager.instance.maxAltitude.ToString("F2");
        }
    }
}
