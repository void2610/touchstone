namespace NEquipment
{
    using System.Collections;
    using System.Collections.Generic;
    using UnityEngine;
    using UnityEngine.UI;
    using NCharacter;

    public class Jet : Equipment
    {
        [SerializeField]
        private ParticleSystem jetParticle;
        protected override void Awake()
        {
            base.Awake();
            equipmentName = "Jet";
            coolTimeLength = 15f;
            activeTimeLength = 3f;
            isHold = false;
        }

        protected override void OnActionStart()
        {
            base.OnActionStart();
            jetParticle.Play();
        }

        protected override void Effect()
        {
            base.Effect();
            //TODO: 2つ以上同時発動した時に対応
            Vector2 now = player.GetComponent<Rigidbody2D>().velocity;
            player.GetComponent<Rigidbody2D>().velocity = Vector2.Lerp(now, new Vector2(now.x, 15.0f * intensity), 0.1f);
            this.transform.position = player.transform.position;
        }

        protected override void OnActionEnd()
        {
            base.OnActionEnd();
            jetParticle.Stop();
        }
    }
}
