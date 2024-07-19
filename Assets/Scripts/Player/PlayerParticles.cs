namespace NCharacter
{
    using UnityEngine;
    using System.Collections.Generic;
    using NManager;

    public class PlayerParticles : MonoBehaviour
    {
        [Header("Audios")]
        [SerializeField]
        private List<AudioClip> killSeClips = new List<AudioClip>();
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
        private int killSePitch = 0;
        private float killComboLimit = 1.5f;
        private float lastKillTime = 0.0f;

        public void PlayJumpSe()
        {

            if (Time.time - lastKillTime < killComboLimit)
            {
                if (killSePitch < 4)
                {
                    killSePitch++;
                }
            }
            else
            {
                killSePitch = 0;
            }
            lastKillTime = Time.time;
            SoundManager.instance.PlaySe(killSeClips[killSePitch]);
        }

        public void PlayDamageSe()
        {
            SoundManager.instance.PlaySe("damage");
        }

        private void StopDashParticle()
        {
            dashParticleInstance.GetComponent<ParticleSystem>().Stop();
        }

        public void PlayDashParticle(Vector3 direction)
        {
            var p = Instantiate(dashParticle, this.transform.position, Quaternion.identity);
            p.transform.rotation = Quaternion.Euler(0, 0, Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg);
            // Invoke("StopDashParticle", dashTime + 0.5f);
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
            // dashParticleInstance = Instantiate(dashParticle, transform.position + legOffset, Quaternion.identity, this.transform);
            // dashParticleInstance.GetComponent<ParticleSystem>().Stop();
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
