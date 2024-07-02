namespace NCharacter
{
    using UnityEngine;

    public class PlayerParticles : MonoBehaviour
    {
        [SerializeField]
        private GameObject sandParticle;
        [SerializeField]
        private GameObject dashParticle;
        [SerializeField]
        private GameObject healParticle;
        [SerializeField]
        private GameObject jetParticle;
        private GameObject dashParticleInstance;
        private GameObject jetParticleInstance;
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

        public void PlayHealParticle()
        {
            Instantiate(healParticle, this.transform.position, Quaternion.identity, this.transform);
        }

        public void PlayJetParticle()
        {
            jetParticleInstance.GetComponent<ParticleSystem>().Play();
        }

        public void StopJetParticle()
        {
            jetParticleInstance.GetComponent<ParticleSystem>().Stop();
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
            jetParticleInstance = Instantiate(jetParticle, transform.position + legOffset, Quaternion.identity, this.transform);
            jetParticleInstance.GetComponent<ParticleSystem>().Stop();
        }

        private void OnCollisionEnter2D(Collision2D collision)
        {
            if (collision.gameObject.tag == "Ground")
            {
                Destroy(Instantiate(sandParticle, transform.position + legOffset, Quaternion.identity), 3.0f);
            }
        }
    }
}
