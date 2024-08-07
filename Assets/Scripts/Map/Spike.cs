namespace NMap
{
    using UnityEngine;
    using NManager;
    using NCharacter;

    public class Spike : MonoBehaviour
    {
        private void OnTriggerEnter2D(Collider2D collision)
        {
            Debug.Log(collision.gameObject.name);
            var p = collision.gameObject.GetComponent<Player>();
            if (p != null)
            {
                p.CutHp(1);
                p.JumpByEnemy(1.5f);
            }
        }
    }
}
