namespace NTitle
{
    using UnityEngine;
    using TMPro;

    public class CoinUI : MonoBehaviour
    {
        private TextMeshProUGUI coinText => this.GetComponent<TextMeshProUGUI>();

        void Update()
        {
            coinText.text = ":" + PlayerPrefs.GetInt("Coin", 0);
        }
    }
}
