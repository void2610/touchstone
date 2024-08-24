namespace NMap
{
    using UnityEngine;
    using NManager;

    public class FloorTrigger : MonoBehaviour
    {
        private void OnCollisionEnter2D(Collision2D collision)
        {
            if (collision.gameObject.CompareTag("PlayerTrigger") && GameManager.instance.maxAltitude > 5 + GameManager.instance.altitudeOffset)
            {
                GameManager.instance.player.CutHp(1);
                GameManager.instance.GetComponent<MapManager>().SetUp(false);
                GameManager.instance.ResetAltitude();
            }
        }
    }
}
