namespace NMap
{
    using UnityEngine;
    using NManager;

    public class FloorTrigger : MonoBehaviour
    {
        private void OnCollisionEnter2D(Collision2D collision)
        {
            if (collision.gameObject.CompareTag("PlayerTrigger") && GameManager.instance.maxAltitude > 30)
            {
                GameManager.instance.player.CutHp(10);
            }
        }
    }
}
