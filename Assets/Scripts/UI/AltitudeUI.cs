namespace NUI
{
    using UnityEngine;
    using UnityEngine.UI;
    using NManager;

    public class AltitudeUI : MonoBehaviour
    {
        private Text altitudeText;
        [SerializeField]
        private string prefix = "max: ";
        void Start()
        {
            altitudeText = gameObject.GetComponent<Text>();
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
