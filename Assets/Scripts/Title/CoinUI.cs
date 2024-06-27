namespace NTitle
{
    using UnityEngine;
    using TMPro;

    public class CoinUI : MonoBehaviour
    {
        private TextMeshProUGUI coinText;
        void Start()
        {
            coinText = GetComponent<TextMeshProUGUI>();
            coinText.text = ":0";
        }

        void Update()
        {
            coinText.text = ":" + PlayerPrefs.GetInt("Coin", 0);
        }
    }
}
