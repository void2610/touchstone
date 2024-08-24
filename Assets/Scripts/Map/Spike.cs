namespace NMap
{
    using UnityEngine;
    using NManager;
    using NCharacter;

    public class Spike : MonoBehaviour
    {
        private void OnTriggerEnter2D(Collider2D collision)
        {
            var p = collision.gameObject.GetComponent<Player>();
            if (p != null && !p.isInvincible && p.isMovable)
            {
                p.CutHp(1);
                p.JumpByEnemy(1.5f);
            }
        }
    }
}
