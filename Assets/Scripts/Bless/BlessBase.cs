namespace NBless
{
    using UnityEngine;

    public class BlessBase : MonoBehaviour
    {
        public string blessName;
        public Color color;
        public Texture2D icon;

        protected void Awake()
        {
            blessName = GetType().Name;
            float size = 100.0f / icon.width;
            this.transform.localScale = new Vector3(size, size, size);
            var i = this.transform.Find("Icon").gameObject.GetComponent<SpriteRenderer>();
            i.sprite = Sprite.Create(icon, new Rect(0, 0, icon.width, icon.height), new Vector2(0.5f, 0.5f));
            i.material.SetColor("_Color", color * 1.5f);
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
