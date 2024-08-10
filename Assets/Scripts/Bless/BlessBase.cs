namespace NBless
{
    using UnityEngine;

    public class BlessBase : MonoBehaviour
    {
        public string blessName;
        public Color color;
        public float emmision;
        public Texture2D icon;
        private Vector3 basePosition;
        private GameObject player;

        private float speed = 1.0f;
        private float speed2 = 1.0f;

        public void SetBasePosition(Vector3 pos, GameObject player)
        {
            basePosition = pos;
            this.player = player;
            speed = Random.Range(0.5f, 3f);
            speed2 = Random.Range(0.1f, 1.5f);
        }

        protected void Awake()
        {
            blessName = GetType().Name;
            float size = 100.0f / icon.width;
            this.transform.localScale = new Vector3(size, size, size);
            var i = this.transform.Find("Icon").gameObject.GetComponent<SpriteRenderer>();
            i.sprite = Sprite.Create(icon, new Rect(0, 0, icon.width, icon.height), new Vector2(0.5f, 0.5f));
            i.material.SetColor("_Color", color * emmision);

            var p = this.transform.Find("Particle").gameObject.GetComponent<ParticleSystem>();
            var renderer = p.GetComponent<Renderer>();
            renderer.material.SetColor("_Color", color * emmision);
        }

        protected void Update()
        {
            if (player == null) return;
            Vector3 target = basePosition + player.transform.position;
            float distance = Vector3.Distance(this.transform.position, target);

            this.transform.position = Vector3.Lerp(this.transform.position, target, (distance / (5 * speed)) * Time.deltaTime * speed2);

            //ゆらゆら上下に揺れる
            float y = Mathf.Sin((Time.time + speed) * 2) * 0.005f * speed;
            this.transform.position += new Vector3(0, y, 0);
        }

        public virtual void OnActive()
        {
        }

        public virtual void OnDeactive()
        {
        }

        public virtual void OnUpdate()
        {
        }

        public virtual void OnPlayerDamaged()
        {
        }

        public virtual void OnPlayerHealed()
        {
        }

        public virtual void OnPlayerDead()
        {
        }

        public virtual void OnPlayerJumped()
        {
        }

        public virtual void OnPlayerLanded()
        {
        }

        public virtual void OnPlayerMoved()
        {
        }

        public virtual void OnPlayerAttacked()
        {
        }

        public virtual void OnPlayerKilledEnemy()
        {
        }

        public virtual void OnStageCleared()
        {
        }
    }
}
