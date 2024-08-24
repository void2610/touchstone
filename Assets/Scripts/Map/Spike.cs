namespace NMap
{
    using UnityEngine;
    using NManager;
    using NCharacter;

    public class Spike : MonoBehaviour
    {
        [SerializeField]
        private bool isUp = true;
        private void OnTriggerEnter2D(Collider2D collision)
        {
            var p = collision.gameObject.GetComponent<Player>();
            if (p != null && !p.isInvincible && p.isMovable)
            {
                p.CutHp(1);
                p.AddForce(new Vector2(0, isUp ? 26.25f : -26.25f));
            }
        }
    }
}
