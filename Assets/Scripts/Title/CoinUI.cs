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
            coinText.text = "Coins: 0";
        }

        void Update()
        {
            coinText.text = "Coins: " + PlayerPrefs.GetInt("Coins", 0);
        }
    }
}
