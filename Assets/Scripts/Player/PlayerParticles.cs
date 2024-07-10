namespace NCharacter
{
    using UnityEngine;
    using NManager;

    public class PlayerParticles : MonoBehaviour
    {
        [Header("Particles")]
        [SerializeField]
        private GameObject sandParticle;
        [SerializeField]
        private GameObject dashParticle;
        [SerializeField]
        private GameObject healParticle;
        private GameObject dashParticleInstance;
        private Vector3 legOffset = new Vector3(0, -1f, 0);
        private SpriteRenderer spriteRenderer;

        public void PlayJumpSe()
        {
            SoundManager.instance.PlaySe("jump");
        }

        public void PlayDamageSe()
        {
            SoundManager.instance.PlaySe("damage");
        }

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
                SoundManager.instance.PlaySe("sand");
            }
        }
    }
}
