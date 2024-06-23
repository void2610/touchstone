using UnityEngine;

public class PlayerParticles : MonoBehaviour
{
    [SerializeField]
    private GameObject sandParticle;
    [SerializeField]
    private GameObject dashParticle;
    private GameObject dashParticleInstance;
    private Vector3 legOffset = new Vector3(0, -1f, 0);
    private SpriteRenderer spriteRenderer;

    private void StopDashParticle()
    {
        dashParticleInstance.GetComponent<ParticleSystem>().Stop();
    }

    public void PlayDashParticle(float dashTime)
    {
        dashParticleInstance.GetComponent<ParticleSystem>().Play();
        Invoke("StopDashParticle", dashTime + 0.5f);
    }

    private void ChangeColorToWhite()
    {
        spriteRenderer.color = Color.white;
    }

    public void ChangeColorToRed(float time = 0.3f)
    {
        spriteRenderer.color = Color.red;
        Invoke("ChangeColorToWhite", time);
    }

    private void Start()
    {
        spriteRenderer = this.GetComponent<SpriteRenderer>();
        dashParticleInstance = Instantiate(dashParticle, transform.position + legOffset, Quaternion.identity, this.transform);
        dashParticleInstance.GetComponent<ParticleSystem>().Stop();
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Ground")
        {
            Destroy(Instantiate(sandParticle, transform.position + legOffset, Quaternion.identity), 3.0f);
        }
    }
}
