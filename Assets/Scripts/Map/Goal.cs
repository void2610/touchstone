namespace NMap
{
    using UnityEngine;
    using NCharacter;
    using NManager;

    public class Goal : MonoBehaviour
    {
        private void OnTriggerEnter2D(Collider2D collision)
        {
            if (collision.gameObject.GetComponent<Player>() != null)
            {
                GameManager.instance.ClearStage();
            }
        }
    }
}
