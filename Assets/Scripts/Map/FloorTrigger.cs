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
                GameManager.instance.player.ChangeMovable(false);
                GameManager.instance.GetComponent<UIManager>().CrossFade(1.0f, () =>
                {
                    GameManager.instance.GetComponent<MapManager>().SetUp(false);
                    GameManager.instance.ResetAltitude();
                    GameManager.instance.player.ChangeMovable(true);
                }, () => { });
            }
        }
    }
}
