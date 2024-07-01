namespace NEquipment
{
    using System.Collections;
    using System.Collections.Generic;
    using UnityEngine;
    using UnityEngine.UI;
    using NCharacter;

    public class Jet : Equipment
    {
        protected override void Awake()
        {
            base.Awake();
            equipmentName = "Jet";
            coolTimeLength = 10f;
            activeTimeLength = 3f;
        }

        protected override void OnActionStart()
        {
            base.OnActionStart();
            player.GetComponent<PlayerParticles>().PlayJetParticle();
        }

        protected override void Effect()
        {
            base.Effect();
            Vector2 now = player.GetComponent<Rigidbody2D>().velocity;
            player.GetComponent<Rigidbody2D>().velocity = Vector2.Lerp(now, new Vector2(now.x, 15), 0.1f);
        }
    }
}
