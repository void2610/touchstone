using UnityEngine;

public class PlayerParticles : MonoBehaviour
{
    [SerializeField]
    private GameObject sandParticle;
    private Vector3 legOffset = new Vector3(0, -1f, 0);

    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Ground")
        {
            Destroy(Instantiate(sandParticle, transform.position + legOffset, Quaternion.identity), 3.0f);
        }
    }
}
